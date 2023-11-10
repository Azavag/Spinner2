using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [SerializeField] GravityAttractor gravityAttractor;
    Transform bodyTransform;
    Rigidbody rb;
    void Start()
    {
        bodyTransform = transform;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        gravityAttractor.Attract(bodyTransform);
    }
}
