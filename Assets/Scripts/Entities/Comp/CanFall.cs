using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Entity))]
public class CanFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling = false;
    private Entity ent;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ent = GetComponent<Entity>();
    }

    public void fall()
    {
        ent.Die();
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
                fall();
            }
        }
    }
}
