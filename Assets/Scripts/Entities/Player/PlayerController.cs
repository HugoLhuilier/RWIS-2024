using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovesController))]
public class PlayerController : Entity
{
    GameManager gameManager;

    private PlayerMovesController move;
    private Animator animator;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        animator = GetComponent<Animator>();
        move = GetComponent<PlayerMovesController>();
    }

/*    private void Update()
    {
        Debug.DrawRay(Vector3.zero,
            transform.position);
    }*/

    public override void Die()
    {
        gameManager.Lose();
    }

    public void getHit()
    {
        animator.SetTrigger("Die");
        move.canMove = false;

        RoomManager[] rooms = FindObjectsOfType<RoomManager>();
        foreach (RoomManager room in rooms)
        {
            room.deactivateRoom();
        }
    }
}
