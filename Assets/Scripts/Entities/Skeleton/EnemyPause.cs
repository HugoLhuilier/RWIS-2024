using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPause : PausingObject
{
    private EnemyStateController controller;

    protected override void Start()
    {
        base.Start();

        controller = GetComponent<EnemyStateController>();
    }

    protected override void OnPause()
    {
        controller.PauseStateMachine();
    }

    protected override void OnResume()
    {
        controller.ResumeStateMachine();
    }
}
