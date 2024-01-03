using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : BaseObstacle
{
    private float _bounceForce = 20f;

    protected void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionDirection = collision.contacts[0].point - collision.gameObject.transform.position;
            Vector3 reflectDirection = Vector3.Reflect(collisionDirection, collision.contacts[0].normal).normalized;
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.AddForce((reflectDirection /*+ Vector3.up * 10f*/) * _bounceForce);
        }
    }
}
