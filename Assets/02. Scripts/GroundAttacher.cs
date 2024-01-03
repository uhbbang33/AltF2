using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttacher : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;// (collision.transform, false);
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
    }

    private void Update()
    {
        // Debug.Log($"Velocity {GetComponent<Rigidbody>().velocity}");
    }
}
