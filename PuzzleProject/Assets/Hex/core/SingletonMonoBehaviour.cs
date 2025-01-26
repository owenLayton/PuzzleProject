using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance()
    {
        return g_instance;
    }

    static T g_instance;

    public void Awake()
    {
        if (g_instance != null && g_instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            g_instance = (T)this;
        }
    }
}
