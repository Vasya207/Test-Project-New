using System;
using UnityEngine;

namespace Core
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public event Action OnAwake;
        
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null) SetupInstance();
                }

                return instance;
            }
        }

        public void Awake()
        {
            OnAwake?.Invoke();
            RemoveDuplicates();
        }

        private static void SetupInstance()
        {
            instance = (T)FindObjectOfType(typeof(T));
            if (instance == null)
            {
                var gameObj = new GameObject();
                gameObj.name = typeof(T).Name;
                instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }

        private void RemoveDuplicates()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}