using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float _jumpforce = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 forwardDirection = collision.transform.up;
        if (otherRigidbody != null)
        {
            otherRigidbody.AddForce(forwardDirection * 30, ForceMode.Impulse);
        }
    }
}
