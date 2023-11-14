// Author: ZWave
// Time: 2023/11/10 16:30
// --------------------------------------------------------------------------

using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }


        protected virtual void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
            }
        }
    }
}