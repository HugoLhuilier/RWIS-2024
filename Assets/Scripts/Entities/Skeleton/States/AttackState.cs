using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float timer = 0;
    bool attackTriggered = false;
    Vector3 playerPosAtAttackTrigger;

    public override void EnterState(EnemyStateController stateController)
    {
        timer = 0;
        attackTriggered = false;
        playerPosAtAttackTrigger = stateController.player.position;
    }

    public override void ExitState(EnemyStateController stateController)
    {
        // Nothing
    }

    public override void UpdateState(EnemyStateController stateController)
    {
        if (!attackTriggered && timer > stateController.attackDelay)
        {
            RaycastHit2D hit = Physics2D.CircleCast(stateController.transform.position,
                stateController.attackRadius,
                playerPosAtAttackTrigger - stateController.transform.position,
                stateController.range,
                LayerMask.GetMask("Player"));

            PlayerController player = hit.collider.GetComponent<PlayerController>();

            if (player != null){
                player.getHit();
            }

            attackTriggered = true;
        }

        if (timer > stateController.attackTime)
        {
            stateController.SwitchState(stateController.followState);
        }

        timer += Time.deltaTime;
    }
}
