using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    private BaseState currentState;
    private bool isPaused = false;
    [SerializeField] private bool startByFollowing = false; // Debug only

    public IdleState idleState = new IdleState();
    public FollowState followState = new FollowState();
    public AttackState attackState = new AttackState();

    // Every n frames, the path from the enemy to the player will be recalculated
    public int framesPathRecalculation;
    public float range;

    // Delay between the moment the player is in the range of the enemy and the moment the cast is done
    public float attackDelay;

    // Total time of the attack state before starting following the player again
    public float attackTime;

    // Radius of the casted circle for the attack
    public float attackRadius;

    public NavMeshAgent agent { get; private set; }

    public Transform player {  get; private set; }

    private void Awake()
    {
        currentState = idleState;
        if (startByFollowing)
            currentState = followState;

        player = FindAnyObjectByType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentState.EnterState(this);
    }

    private void Update()
    {
        if (!isPaused)
            currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    public void BecomeIdle()
    {
        SwitchState(idleState);
    }

    public void StartFollowing()
    {
        SwitchState(followState);
    }

    public void PauseStateMachine()
    {
        isPaused = true;
    }

    public void ResumeStateMachine()
    {
        isPaused = false;
    }
}
