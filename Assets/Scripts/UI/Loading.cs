using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Loading : MonoBehaviour
    {
        
        public float loadTimer = 10f;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Text text;

        private float _varTimer;
        private void Update()
        {
            _varTimer += Time.deltaTime;
            if (_varTimer < loadTimer)
            {
                slider.value = _varTimer * 10;
                text.text = (int)slider.value + "%";
            }
            else
            {
                slider.value = 100;
                text.text = "100%";
                SceneManager.LoadScene(2);
            }
        }
        
    }
}
