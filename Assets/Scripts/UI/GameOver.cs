using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
            PlayerHealth.Instance.DeathEvent += () => gameObject.SetActive(true);
        }

        public void ReStartEvent()
        {
            SceneManager.LoadScene(2);
        }

        public void BackEvent()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitEvent()
        {
            Application.Quit(0);
        }
    }
}