using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PausingObject : MonoBehaviour
{
    private GameManager gameManager;

    protected virtual void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        gameManager.pauseTime.AddListener(OnPause);
        gameManager.resumeTime.AddListener(OnResume);
    }

    protected abstract void OnPause();
    protected abstract void OnResume();
}
