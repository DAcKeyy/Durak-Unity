// -------------------------------------------------------------------------------------------
// The MIT License
// MonoCache is a fast optimization framework for Unity https://github.com/MeeXaSiK/MonoCache
// Copyright (c) 2021 Night Train Code
// -------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using Miscellaneous.Console;
using Miscellaneous.Optimization.System;
using UnityEngine;

namespace Miscellaneous.Optimization.MonoBehaviourCache
{
    [DisallowMultipleComponent]
    public sealed class GlobalUpdate : Singleton<GlobalUpdate>
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private const string ON_ENABLE = nameof(ON_ENABLE);
        private const string ON_DISABLE = nameof(ON_DISABLE);
        
        private const string UPDATE_NAME = nameof(Update);
        private const string FIXED_UPDATE_NAME = nameof(FixedUpdate);
        private const string LATE_UPDATE_NAME = nameof(LateUpdate);

        private const BindingFlags METHOD_FLAGS = BindingFlags.Public | 
                                                 BindingFlags.NonPublic | 
                                                 BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;

        private void Awake()
        {
            CheckForErrors();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
        
        private static void CheckForErrors()
        {
            var subclassTypes = Assembly
                .GetAssembly(typeof(MonoBehaviourCache))
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(MonoBehaviourCache)));
            
            foreach (var type in subclassTypes)
            {
                if(type == typeof(GlobalUpdate)) continue;

                var methods = type.GetMethods(METHOD_FLAGS);
                
                foreach (var method in methods)
                {
                    switch (method.Name)
                    {
                        case ON_ENABLE:
                            Debug.LogException(new Exception(
                                $"{TextExtentions.GetExceptionBaseText(ON_ENABLE, type.Name)}" +
                                $"{TextExtentions.GetColoredHtmlText(TextExtentions.BLUE_COLOR, "protected override void")} " +
                                $"{TextExtentions.GetColoredHtmlText(TextExtentions.ORANGE_COLOR, "OnEnabled()")}"));
                            break;
                        case ON_DISABLE:
                            Debug.LogException(new Exception(
                                $"{TextExtentions.GetExceptionBaseText(ON_DISABLE, type.Name)}" +
                                $"{TextExtentions.GetColoredHtmlText(TextExtentions.BLUE_COLOR, "protected override void")} " +
                                $"{TextExtentions.GetColoredHtmlText(TextExtentions.ORANGE_COLOR, "OnDisabled()")}"));
                            break;
                        case UPDATE_NAME:
                            Debug.LogWarning(
                                TextExtentions.GetWarningBaseText(
                                    method.Name, "Run()", type.Name));
                            break;
                        case FIXED_UPDATE_NAME:
                            Debug.LogWarning(
                                TextExtentions.GetWarningBaseText(
                                    method.Name, "FixedRun()", type.Name));
                            break;
                        case LATE_UPDATE_NAME:
                            Debug.LogWarning(
                                TextExtentions.GetWarningBaseText(
                                    method.Name, "LateRun()", type.Name));
                            break;
                    }
                }
            }
        }
    }
}