using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GyroscopeReader))]
public class PlayerMovesController : MonoBehaviour
{
    [SerializeField] private float maxForce;

    private Rigidbody2D rb;
    private GyroscopeReader gyr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gyr = GetComponent<GyroscopeReader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        rb.AddForce(maxForce * gyr.getTilt());
    }
}
