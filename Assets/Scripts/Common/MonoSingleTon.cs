using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
    public class MonoSingleTon<T> :MonoBehaviour where T: MonoSingleTon<T>
    {

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                        instance = new GameObject(typeof(T) + "SingleTon").AddComponent<T>();
                    else
                        instance.Init();
                }
                return instance;
            }
        }

        protected void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }

        public virtual void Init()
        {
            
        }
    }
}
