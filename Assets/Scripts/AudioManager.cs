using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class AudioManager : Singleton<AudioManager>
{
    
    [SerializeField]
    private List<AudioClip> soundList;
    private Dictionary<string, AudioClip> _allSoundClips;
    
    [SerializeField]
    private List<AudioClip> bgmList;
    private Dictionary<string, AudioClip> _allBGMClips;
    
    public int BGMCount => _allBGMClips.Count;
    public int SoundCount => _allSoundClips.Count;

    public float BGMVolume { get; private set; }
    public float SoundVolume { get; private set; }

    // 音效AudioSource缓存池
    private SourceCache _sourceCache;
    // 音效正在播放列表
    private List<AudioSource> _playingList;
    // 背景音乐AudioSource
    private AudioSource _bgmSource;
    
    
    protected override void Awake()
    {
        base.Awake();
        _sourceCache = new SourceCache(gameObject);
        _playingList = new List<AudioSource>();
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        BGMVolume = SoundVolume = 0.75f;
        LoadAll();
    }

    private void Update()
    {
        PlayingGiveBack();
    }

    
    private void LoadAll()
    {
        _allSoundClips = new Dictionary<string, AudioClip>();
        _allBGMClips = new Dictionary<string, AudioClip>();

        foreach (var clip in soundList)
        {
            if (_allSoundClips.TryAdd(clip.name, clip))
            {
                Debug.Log($"音频管理器---》成功注册音效{clip.name}");
            }
            else
            {
                Debug.LogWarning($"音频管理器---》音效{clip.name}注册失败！");
            }
        }
        
        foreach (var clip in bgmList)
        {
            if (_allBGMClips.TryAdd(clip.name, clip))
            {
                Debug.Log($"音频管理器---》成功注册背景音乐{clip.name}");
            }
            else
            {
                Debug.LogWarning($"音频管理器---》背景音乐{clip.name}注册失败！");
            }
        }

    }


    /// ----------------------------------------------------------------------------------------------------------------
    /// 背景音乐控制
    /// ----------------------------------------------------------------------------------------------------------------
    
    public AudioSource PlayBGM(string bgmInfo)
    {
        if (_allBGMClips.TryGetValue(bgmInfo, out var clip))
        {
            _bgmSource.clip = clip;
            _bgmSource.volume = BGMVolume;
            _bgmSource.Play();
        }
        else
        {
            var msg = $"音频管理器---》未能找到名称为{bgmInfo}的背景音乐，无法播放";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
        return _bgmSource;
    }

    public AudioSource PlayBGM(int index)
    {
        var clip = _allBGMClips.ElementAt(index).Value;
        if (clip == null)
        {
            var msg = $"音频管理器---》未能通过索引{index}找到对应的背景音乐，无法播放";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
        _bgmSource.clip = clip;
        _bgmSource.volume = BGMVolume;
        _bgmSource.Play();
        return _bgmSource;
    }

    public AudioSource PauseBGM()
    {
        _bgmSource.Pause();
        return _bgmSource;
    }
    
    public AudioSource StopBGM()
    {
        _bgmSource.Stop();
        return _bgmSource;
    }
    
    public void SetBGMVolume(float volume)
    {
        _bgmSource.volume = BGMVolume = volume;
    }

    /// ----------------------------------------------------------------------------------------------------------------
    /// 音效控制
    /// ----------------------------------------------------------------------------------------------------------------
    
    public void PlaySound(string soundInfo, bool isLoop = false, Action<AudioSource> callback = null)
    {
        var source = _sourceCache.Get();
        if (_allSoundClips.TryGetValue(soundInfo, out var clip))
        {
            source.clip = clip;
        }
        else
        {
            var msg = $"音频管理器---》未能找到名称为{soundInfo}的音效，无法播放";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
        source.loop = isLoop;
        source.volume = SoundVolume;
        source.Play();
        _playingList.Add(source);
        callback?.Invoke(source);
    }

    public void StopSound(AudioSource source)
    {
        if (_playingList.Contains(source)) {
            source.Stop();
            _sourceCache.Push(source);
            _playingList.Remove(source);
        }
        else
        {
            var msg = $"音频管理器---》未在播放列表中找到音效{source.name}，无法暂停";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
    }
    
    public void SetAllSoundVolume(float volume)
    {
        SoundVolume = volume;
        foreach (var source in _playingList)
        {
            source.volume = SoundVolume;
        }
    }

    private void PlayingGiveBack()
    {
        for (var i = 0; i < _playingList.Count; i++)
        {
            var source = _playingList[i];
            if (source.isPlaying)
            {
                return;
            }
            _sourceCache.Push(source);
            _playingList.RemoveAt(i);
        }
    }

    private class SourceCache: ObjectPool<AudioSource>
    {
        private readonly GameObject _soundObject;
        
        public SourceCache(GameObject soundObject)
        {
            _soundObject = soundObject;
        }
        
        public override void Push(AudioSource obj)
        {
            base.Push(obj);
            obj.enabled = false;
        }
        
        public override AudioSource Get()
        {
            AudioSource audioSource;
            if (IsNotEmpty())
            {
                audioSource = Pool.Dequeue();
                audioSource.enabled = true;
            }
            else
            {
                audioSource = _soundObject.AddComponent<AudioSource>();
            }
            return audioSource;
        }

    }

}