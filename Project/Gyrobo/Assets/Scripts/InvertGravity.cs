using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravity : MonoBehaviour
{
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        ReverseGravity();
    }

    void FixedUpdate()
    {
        rb.AddForce(ReverseGravity(), ForceMode.Acceleration);
    }

    Vector3 ReverseGravity() => Physics.gravity * -1;
}
