using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    private BaseState currentState;
    private BaseState stateBeforePause;
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

    public Animator anim { get; private set; } //for animation
    private Vector2 lastMoveDirection;

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


        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isPaused)
            currentState.UpdateState(this);

        Animate();
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
        stateBeforePause = currentState;

        SwitchState(idleState);

        isPaused = true;
    }

    public void ResumeStateMachine()
    {
        SwitchState(stateBeforePause);
        stateBeforePause = null;

        isPaused = false;
    }


    void Animate()
    {
        if (agent.velocity.magnitude > 0.1)
        {
            // Moving: Update animation to reflect direction and movement
            anim.SetFloat("SkMoveX", agent.velocity.x);
            anim.SetFloat("SkMoveY", agent.velocity.y);
            anim.SetFloat("SkMoveMagnitude", agent.velocity.magnitude);

            // Store the last move direction for idle state
            lastMoveDirection = agent.velocity;
        }
        else
        {
            // Idle: Ensure "LastMoveX" and "LastMoveY" are set
            anim.SetFloat("SkLastMoveX", lastMoveDirection.x);
            anim.SetFloat("SkLastMoveY", lastMoveDirection.y);
        }
    }
}

