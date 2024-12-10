using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Entity))]
public class CanFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void fall()
    {
        Debug.Log("Je tooooombe");
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
