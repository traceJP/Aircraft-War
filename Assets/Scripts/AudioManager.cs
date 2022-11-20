using System;
using System.Collections.Generic;
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

    private float _bgmVolume;
    private float _soundVolume;

    // 音效AudioSource缓存池
    private SourceCache _sourceCache;
    // 音效正在播放列表
    private Dictionary<string, AudioSource> _playingList;
    // 背景音乐AudioSource
    private AudioSource _bgmSource;
    
    
    protected override void Awake()
    {
        base.Awake();
        _sourceCache = new SourceCache(gameObject);
        _playingList = new Dictionary<string, AudioSource>();
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmVolume = _soundVolume = 1;
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
    
    public void PlayBGM(string bgmInfo)
    {
        if (_allBGMClips.TryGetValue(bgmInfo, out var clip))
        {
            _bgmSource.clip = clip;
            _bgmSource.volume = _bgmVolume;
            _bgmSource.Play();
        }
        else
        {
            var msg = $"音频管理器---》未能找到名称为{bgmInfo}的背景音乐，无法播放";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
        
    }

    public void PauseBGM()
    {
        _bgmSource.Pause();
    }
    
    public void StopBGM()
    {
        _bgmSource.Stop();
    }
    
    public void SetBGMVolume(float volume)
    {
        _bgmSource.volume = _bgmVolume = volume;
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
        source.volume = _soundVolume;
        source.Play();
        _playingList.Add(soundInfo, source);
        callback?.Invoke(source);
    }

    public void StopSound(string soundInfo)
    {
        if (_playingList.TryGetValue(soundInfo, out var source)) {
            source.Stop();
            _sourceCache.Push(source);
            _playingList.Remove(soundInfo);
        }
        else
        {
            var msg = $"音频管理器---》音效{soundInfo}未在播放，无法暂停";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
    }
    
    public void SetAllSoundVolume(float volume)
    {
        _soundVolume = volume;
        foreach (var (_, value) in _playingList)
        {
            value.volume = _soundVolume;
        }
    }

    private void PlayingGiveBack()
    {
        foreach (var source in _playingList)
        {
            if (source.Value.isPlaying)
            {
                return;
            }
            _sourceCache.Push(source.Value);
            _playingList.Remove(source.Key);
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