using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Setting : MonoBehaviour
    {

        private float _originalVolume;

        [SerializeField]
        private Slider volumeSlider;

        private void Awake()
        {
            volumeSlider.minValue = 0;
            volumeSlider.maxValue = 1;
        }

        public void ActiveEvent(bool status)
        {
            gameObject.SetActive(status);
        }

        public void VolumeToggle(bool isMute)
        {
            if (isMute)
            {
                _originalVolume = volumeSlider.value;
                
                // TODO：禁用滑动条
                
                AudioManager.Instance.SetBGMVolume(0);
                AudioManager.Instance.SetAllSoundVolume(0);
            }
            else
            {
                AudioManager.Instance.SetBGMVolume(_originalVolume);
                AudioManager.Instance.SetAllSoundVolume(_originalVolume);
            }
        }

        // TODO: 要加个参数
        public void VolumeSliderChange()
        {
            
        }
        
        
    }
}
