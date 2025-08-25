using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T:SingletonBase<T>
{
    static public SingletonBase<T> instance;

    public virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        
        //确保跨场景时仍然是单例类
        DontDestroyOnLoad(gameObject);
    }

    public void OnDestroy()
    {
        instance = null;
    }
}
