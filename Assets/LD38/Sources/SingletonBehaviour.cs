using System;
using UnityEngine;

namespace LD38 {
    public class SingletonBehaviour<T> : MonoBehaviour where T: SingletonBehaviour<T> {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance != null) {
                    return _instance;
                }
                var instances = FindObjectsOfType<T>();
                switch (instances.Length) {
                    case 0:
                        _instance = new GameObject(typeof(T).FullName).AddComponent<T>();
                        break;
                    case 1:
                        _instance = instances[0];
                        break;
                    default:
                        throw new InvalidOperationException($"Too many instances of {typeof(T).FullName} in scene!");
                }
                return _instance;
            }
        }

        protected virtual void Awake() {
            _instance = (T) this;
        }

        protected SingletonBehaviour() {}
    }
}
