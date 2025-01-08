using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : PausingObject
{
    private float elapsedTime = 0;
    private bool timerOn = false;

    protected override void OnPause()
    {
        timerOn = false;
    }

    protected override void OnResume()
    {
        timerOn = true;
    }

    public void startTimer()
    {
        elapsedTime = 0;
        timerOn = true;
    }

    public float stopTimer()
    {
        timerOn = false;
        return elapsedTime;
    }

    public float getTime()
    {
        return elapsedTime;
    }

    private void Update()
    {
        if (timerOn)
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
