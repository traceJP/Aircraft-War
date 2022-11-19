using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Login : MonoBehaviour
    {

        [SerializeField]
        private InputField inputUsername;
    
        [SerializeField]
        private InputField inputPassword;

        [SerializeField]
        private GameObject tipObj;
    
        [SerializeField]
        private GameObject registerForm;


        public void LoginEvent()
        {
            var status = UserInfo.HasUser(inputUsername.text, inputPassword.text);
            if (status)
            {
                Debug.Log("登录成功");
                SceneManager.LoadScene(1);
            }
            else
            {
                inputUsername.text = "666";
                ShowByTip("登录失败，请检查账号密码！", 2);
                inputUsername.text = "";
                inputPassword.text = "";
            }
        }

        private void ShowByTip(string msg, int timer)
        {
            if (tipObj.activeSelf)
            {
                CloseTipAsync();
            }
            else
            {
                tipObj.GetComponent<Text>().text = msg;
                tipObj.SetActive(true);
            }
            Invoke(nameof(CloseTipAsync), timer);
        }
        private void CloseTipAsync()
        {
            tipObj.SetActive(false);
        }

    }
}

