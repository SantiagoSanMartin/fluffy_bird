using UnityEngine;
using System;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return instance != null;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            throw new InvalidOperationException("Tried to instantiate a second singleton of type: " + typeof(T).Name);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
