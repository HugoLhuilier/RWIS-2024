using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GyroscopeReader))]
public class PlayerMovesController : MonoBehaviour
{
    [SerializeField] private float maxForce;

    private Rigidbody2D rb;
    private GyroscopeReader gyr;
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gyr = GetComponent<GyroscopeReader>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();

        if ((input.x < 0 && !facingLeft) || (input.x > 0 && facingLeft))
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Vector2 tilt = gyr.getTilt();
        rb.AddForce(maxForce * tilt);
    }

    void ProcessInputs()
    {
        // Get tilt from the gyroscope
        Vector2 tilt = gyr.getTilt();

        // Map tilt values to input
        input = tilt;

        // Save last move direction if there is movement
        if (input.magnitude > 0.1f) // Avoid noise with a small threshold
        {
            lastMoveDirection = input;
        }

        // Normalize input for consistent movement speed
        input.Normalize();
    }

    void Animate()
    {
        if (input.magnitude > 0.1)
        {
            // Moving: Update animation to reflect direction and movement
            anim.SetFloat("MoveX", input.x);
            anim.SetFloat("MoveY", input.y);
            anim.SetFloat("MoveMagnitude", input.magnitude);

            // Store the last move direction for idle state
            lastMoveDirection = input;
        }
        else
        {
            // Idle: Ensure "LastMoveX" and "LastMoveY" are set
            anim.SetFloat("LastMoveX", lastMoveDirection.x);
            anim.SetFloat("LastMoveY", lastMoveDirection.y);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}

