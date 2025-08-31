using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scirpts.APITool
{
    /// <summary>
    /// 延迟调用工具类
    /// <remarks>特点是不依赖MonoBehaviour，可以在没有继承Mono的类中调用此类</remarks>
    /// </summary>
    public static class DelayCallTool
    {
        // 存储所有延迟任务
        private static readonly List<ScheduledTask> tasks = new List<ScheduledTask>();

        // 调度器必须在 Update 时被调用
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            // 创建一个隐藏 GameObject 来驱动 Update
            var go = new GameObject("DelayCallUpdater");
            go.hideFlags = HideFlags.HideAndDontSave;
            go.AddComponent<DelayUpdater>();
            UnityEngine.Object.DontDestroyOnLoad(go);
        }

        public static void Call(float delaySeconds, Action callback)
        {
            if (callback == null) return;
            tasks.Add(new ScheduledTask
            {
                executeTime = Time.time + delaySeconds,
                callback = callback
            });
        }

        private class DelayUpdater : MonoBehaviour
        {
            void Update()
            {
                if (tasks.Count == 0) return;

                for (int i = tasks.Count - 1; i >= 0; i--)
                {
                    var t = tasks[i];
                    if (Time.time >= t.executeTime)
                    {
                        try { t.callback?.Invoke(); } catch { }
                        tasks.RemoveAt(i);
                    }
                }
            }
        }

        private class ScheduledTask
        {
            public float executeTime;
            public Action callback;
        }
    }
}