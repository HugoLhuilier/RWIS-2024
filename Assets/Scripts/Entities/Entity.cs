using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(PausingObject))]

public abstract class Entity : MonoBehaviour
{
    public abstract void Die();
}
