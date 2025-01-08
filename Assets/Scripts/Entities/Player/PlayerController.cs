using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovesController))]
public class PlayerController : Entity
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public override void Die()
    {
        gameManager.Lose();
    }

    public void getHit()
    {
        // TODO: play the animation and then die

        Die();
    }
}
