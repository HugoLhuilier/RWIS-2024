using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float smoothTime = .2f;

    private Transform player;
    private readonly Vector3 zOffset = new Vector3(0, 0, -10);

    private Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, newPos(), ref vel, smoothTime);
    }

    // Get the expected position of the camera
    private Vector3 newPos()
    {
        return player.position + zOffset;
    }
}
