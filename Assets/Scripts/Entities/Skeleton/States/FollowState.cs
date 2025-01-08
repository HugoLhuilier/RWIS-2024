using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : BaseState
{
    public override void EnterState(EnemyStateController stateController)
    {
        stateController.agent.SetDestination(stateController.player.position);
    }

    public override void ExitState(EnemyStateController stateController)
    {
        stateController.agent.ResetPath();
    }

    public override void UpdateState(EnemyStateController stateController)
    {
        if (stateController.player == null)
        {
            stateController.SwitchState(stateController.idleState);
        }

        if (Time.frameCount % stateController.framesPathRecalculation == 0)
        {
            stateController.agent.SetDestination(stateController.player.position);
        }

        if (!stateController.agent.hasPath && !stateController.agent.pathPending)
        {
            stateController.SwitchState(stateController.idleState);
        }

        if (Vector3.Distance(stateController.transform.position, stateController.player.position) <= stateController.range)
        {
            stateController.SwitchState(stateController.attackState);
        }
    }
}
