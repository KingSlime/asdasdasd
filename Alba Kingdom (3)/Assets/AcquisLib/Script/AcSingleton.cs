using UnityEngine;

namespace AcquisLib
{
    public class AcSingleton<T> : MonoBehaviour where T : AcSingleton<T>
    {
        private static T _instance = null;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this as T)
            {
                Destroy(gameObject);
            }
        }

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    _instance = singleton.AddComponent<T>();
                }
                return _instance;
            }
        }
        public void SingletonDestory()
        {
            Destroy(gameObject);
            _instance = null;
        }
    }
}