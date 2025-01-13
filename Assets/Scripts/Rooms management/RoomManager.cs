using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    public UnityEvent activateRoomEvent;
    public UnityEvent deactivateRoomEvent;

    [SerializeField] private GameObject torchesSpotsParent;
    [SerializeField] private GameObject lampsSpotsParent;
    [SerializeField] private GameObject enemySpawnSpotsParent;
    [SerializeField] private Transform spawnPoint;
    public Transform getSpawnPoint() {  return spawnPoint; }

    [SerializeField] private Transform exitPoint;
    public Transform getExitPoint() { return exitPoint; }

    private Transform[] torchesSpots;
    private Transform[] lampsSpots;
    private Transform[] enemySpawnSpots;

    private GameManager gameManager;

    private void Awake()
    {
        torchesSpots = torchesSpotsParent.GetComponentsInChildren<Transform>();
        lampsSpots = lampsSpotsParent.GetComponentsInChildren<Transform>();
        enemySpawnSpots = enemySpawnSpotsParent.GetComponentsInChildren<Transform>();
        // !!!!! Also contains the gameobject itself, which we don't need !!!!!

        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void activateRoom()
    {
        activateRoomEvent.Invoke();
    }

    public void deactivateRoom()
    {
        deactivateRoomEvent.Invoke();
    }

    // The parameter difficulty is the probability for each enemy spot to spawn a skeleton, and the probability for a light spot not to emit light
    public void GenerateRoom(float difficulty)
    {
        foreach (Transform t in torchesSpots)
        {
            if (t == torchesSpotsParent.transform)
                continue;

            if (Random.Range(0f, 1f) > difficulty)
                Instantiate(gameManager.torchPrefab, t);
        }

        foreach (Transform t in lampsSpots)
        {
            if (t == lampsSpotsParent.transform)
                continue;

            if (Random.Range(0f, 1f) > difficulty)
                Instantiate(gameManager.lampPrefab, t);
        }

        foreach (Transform t in enemySpawnSpots)
        {
            if (t == enemySpawnSpotsParent.transform)
                continue;

            if (Random.Range(0f, 1f) < difficulty)
                Instantiate(gameManager.skeletonPrefab, t);
        }
    }
}
