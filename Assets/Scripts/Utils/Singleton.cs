using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {

        static T m_instance;
        public static T Instance { get => m_instance; }

        protected virtual void Awake()
        {

            if (m_instance == null)
            {

                m_instance = (T)this;

            }

        }

    }
}