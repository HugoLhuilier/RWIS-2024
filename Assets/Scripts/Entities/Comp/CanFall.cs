using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Entity))]
public class CanFall : MonoBehaviour
{
    [SerializeField] private float fallingTime = 1.5f;
    [SerializeField] private float stoppingTime = 0.2f; // Time is seconds needed to stop the movement of the player from the moment it's falling

    private Rigidbody2D rb;
    private bool isFalling = false;
    private float timeSinceFall = 0;
    private Entity ent;
    private SpriteRenderer spriteRenderer;
    private Vector2 finalVel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ent = GetComponent<Entity>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isFalling)
        {
            timeSinceFall += Time.deltaTime;

            if (timeSinceFall > fallingTime)
            {
                ent.Die();
            }


            float frac = 1 - timeSinceFall / fallingTime;
            transform.localScale = frac * Vector3.one;
            spriteRenderer.color = frac * Color.white;

            rb.velocity = Mathf.Clamp01(1 - timeSinceFall / stoppingTime) * finalVel;
        }
    }

    public void startFalling()
    {
        finalVel = rb.velocity;
        rb.isKinematic = true;
        isFalling = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isFalling)
        {
            return;
        }

        if (collision.tag == "Void")
        {
            if (collision.OverlapPoint(transform.TransformPoint(rb.centerOfMass)))
            {
                startFalling();
            }
        }
    }
}
