// ----------------------------------------------------------------
// The MIT License
// Singleton for Unity https://github.com/MeeXaSiK/NightSingleton
// Copyright (c) 2021 Night Train Code
// ----------------------------------------------------------------

using UnityEngine;

namespace Miscellaneous.Optimization.System
{
    public class Singleton<TSingleton> : MonoBehaviourCache.MonoBehaviourCache where TSingleton : MonoBehaviourCache.MonoBehaviourCache
    {
        public static TSingleton Instance => GetNotNull();
        private static TSingleton _cachedInstance;

        public static TSingleton GetCanBeNull()
        {
            if (_cachedInstance != null)
            {
                return _cachedInstance;
            }
            
            var instances = FindObjectsOfType<TSingleton>();
            var instance = instances.Length > 0 ? instances[0] : null;

            for (var i = 1; i < instances.Length; i++)
            {
                Destroy(instances[i]);
            }

            return _cachedInstance = instance;
        }

        public static TSingleton GetNotNull()
        {
            if (_cachedInstance != null)
            {
                return _cachedInstance;
            }
            
            var instances = FindObjectsOfType<TSingleton>();
            var instance = instances.Length > 0 
                ? instances[0] 
                : new GameObject($"[Singleton] {typeof(TSingleton).Name}").AddComponent<TSingleton>();

            for (var i = 1; i < instances.Length; i++)
            {
                Destroy(instances[i]);
            }

            return _cachedInstance = instance;
        }
    }
}