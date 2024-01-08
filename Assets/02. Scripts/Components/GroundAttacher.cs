using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttacher : MonoBehaviour
{
    public LayerMask FootHold;
    public Transform footHold;
    public Vector3 landPosition;
    public Vector3 offset;
    private Rigidbody Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("D");
        if(1 << collision.gameObject.layer == FootHold && footHold == null)
        {
            landPosition = transform.position;
            footHold = collision.gameObject.transform;
            Rigidbody.useGravity = false;
            Rigidbody.velocity = Vector3.zero;            
            offset = landPosition - footHold.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void Update()
    {
        if (footHold)
        {
            transform.position = footHold.position + offset;
        }
    }
}
