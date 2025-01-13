using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Switcher : MonoBehaviour
{
    [SerializeField] private Transform tpTo;
    public Transform getTpTo() { return tpTo; }
    public void setTpTo(Transform newTp) { tpTo = newTp; }

    [SerializeField] private Switcher otherTp;
    public Switcher getOtherSwitcher() { return otherTp; }

    [SerializeField] private RoomManager currentRoom;
    public RoomManager getCurrentRoom() {  return currentRoom; }
    public void setCurrentRoom(RoomManager newCur) { currentRoom = newCur; }

    [SerializeField] private float teleportationTime = 1.0f;
    [SerializeField] private float stopTime = 0.5f;

    private Transform player;
    private Vector3 initPos;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private IEnumerator TeleportCoroutine()
    {
        currentRoom.deactivateRoom();

        // Wait for stopTime seconds
        yield return new WaitForSecondsRealtime(stopTime);

        // Moves the player to the teleport position in teleportationTime seconds
        float passed = 0;

        while (passed <= teleportationTime)
        {
            player.position = Vector3.Lerp(initPos, tpTo.position, Mathf.SmoothStep(0, 1, passed / teleportationTime));
            passed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        // Wait for stopTime other seconds
        yield return new WaitForSecondsRealtime(stopTime);

        gameManager.NextRoom();
        gameManager.ResumeTime();

        otherTp.getCurrentRoom().activateRoom();
    }

    private void startTeleportation()
    {
        initPos = player.position;
        StartCoroutine(TeleportCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() && !gameManager.timePaused)
        {
            gameManager.PauseTime();

            player = collision.transform;
            startTeleportation();
        }
    }
}
