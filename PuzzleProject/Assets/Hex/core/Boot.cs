using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boot : SingletonMonoBehaviour<Boot>
{
    new private void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 99;
    }

    private void Start()
    {
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
}
