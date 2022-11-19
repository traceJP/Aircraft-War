using UnityEngine;

namespace UI
{
    public class Setting : MonoBehaviour
    {

        public void ActiveEvent(bool status)
        {
            gameObject.SetActive(status);
        }
        
        
    }
}
