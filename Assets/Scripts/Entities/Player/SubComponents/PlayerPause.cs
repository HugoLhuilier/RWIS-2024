using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPause : PausingObject
{
    private struct RbInfos
    {
        public Vector2 vel;
        public float angVel;
    }

    private RbInfos rbInfos;
    private Rigidbody2D rb;


    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnPause()
    {
        Debug.Log("Player paused");

        rbInfos.vel = rb.velocity;
        rbInfos.angVel = rb.angularVelocity;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        rb.simulated = false;
    }

    protected override void OnResume()
    {
        Debug.Log("Player resumed");

        rb.simulated = true;

        rb.velocity = rbInfos.vel;
        rb.angularVelocity = rbInfos.angVel;
    }
}
