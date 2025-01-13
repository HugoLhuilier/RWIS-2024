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
        //Debug.Log("Entering Attack state");

        timer = 0;
        attackTriggered = false;
        playerPosAtAttackTrigger = stateController.player.position;

        stateController.anim.SetTrigger("Attacks");
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

/*            Debug.DrawLine(stateController.transform.position,
                stateController.transform.position + (playerPosAtAttackTrigger - stateController.transform.position) * stateController.range,
                Color.green,
                1f);*/

            if (hit.collider)
            {
                PlayerController player = hit.collider.GetComponent<PlayerController>();

                if (player){
                    player.getHit();
                }
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
