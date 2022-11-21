using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Register : MonoBehaviour
    {

        [SerializeField]
        private InputField inputUserName;

        [SerializeField]
        private InputField inputPassword;

        [SerializeField]
        private InputField inputCheckPassword;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void RegisterEvent()
        {
            if (inputPassword.text == inputCheckPassword.text && !UserInfo.HasUser(inputUserName.text, inputPassword.text))
            {
                Debug.Log("注册成功");
                UserInfo.InsertUser(inputUserName.text, inputPassword.text);
                gameObject.SetActive(false);
            }
            inputUserName.text = "";
            inputPassword.text = "";
            inputCheckPassword.text = "";
        }

        public void ActiveEvent(bool status)
        {
            gameObject.SetActive(status);
        }
        
    }
}
