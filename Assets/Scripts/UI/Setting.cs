using Room;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Setting : MonoBehaviour
    {
        
        [SerializeField]
        private Toggle volumeToggle;
        
        [SerializeField]
        private Slider volumeSlider;

        [SerializeField]
        private Text bgmTitle;

        private float _originalVolume;

        private int _bgmIndex;

        private void Awake()
        {
            gameObject.SetActive(false);
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 1;
            volumeSlider.value = AudioManager.Instance.BGMVolume;
            var source = AudioManager.Instance.PlayBGM(_bgmIndex);
            bgmTitle.text = source.clip.name;

            volumeToggle.onValueChanged.AddListener(VolumeToggle);
            volumeSlider.onValueChanged.AddListener(VolumeSliderChange);
        }

        public void ActiveEvent(bool status)
        {
            gameObject.SetActive(status);
        }

        private void VolumeToggle(bool isMute)
        {
            if (isMute)
            {
                volumeSlider.interactable = false;
                _originalVolume = volumeSlider.value;
                volumeSlider.value = 0f;
                AudioManager.Instance.SetBGMVolume(0);
                AudioManager.Instance.SetAllSoundVolume(0);
            }
            else
            {
                volumeSlider.interactable = true;
                volumeSlider.value = _originalVolume;
                AudioManager.Instance.SetBGMVolume(_originalVolume);
                AudioManager.Instance.SetAllSoundVolume(_originalVolume);
            }
        }
        
        private void VolumeSliderChange(float value)
        {
            AudioManager.Instance.SetBGMVolume(value);
            AudioManager.Instance.SetAllSoundVolume(value);
        }
        
        public void MusicLastButton()
        {
            if (--_bgmIndex < 0)
            {
                _bgmIndex = AudioManager.Instance.BGMCount - 1;
            }
            var source = AudioManager.Instance.PlayBGM(_bgmIndex);
            bgmTitle.text = source.clip.name;
        }

        public void MusicNextButton()
        {
            if (++_bgmIndex > AudioManager.Instance.BGMCount - 1)
            {
                _bgmIndex = 0;
            }
            var source = AudioManager.Instance.PlayBGM(_bgmIndex);
            bgmTitle.text = source.clip.name;
        }
        
        public void LevelChangeToggle(int level)
        {
            RoomController.Level = (Level)level;
        }
        
    }
}
