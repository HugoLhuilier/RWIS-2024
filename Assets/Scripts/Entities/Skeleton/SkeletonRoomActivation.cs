using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRoomActivation : RoomActivableObject
{
    private EnemyStateController enemyStateController;

    protected override void Awake()
    {
        base.Awake();

        enemyStateController = GetComponent<EnemyStateController>();
    }

    protected override void onRoomActivate()
    {
        enemyStateController.SwitchState(enemyStateController.followState);

        Debug.Log("Skeleton " + this.ToString() + " activated");
    }

    protected override void onRoomDeactivate()
    {
        enemyStateController.SwitchState(enemyStateController.idleState);

        Debug.Log("Skeleton " + this.ToString() + " deactivated");
    }
}
