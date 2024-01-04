using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : BaseObstacle
{
    private float _bounceForce = 4000f;

    protected void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionDirection = collision.contacts[0].point - collision.gameObject.transform.position;
            Vector3 reflectDirection = Vector3.Reflect(collisionDirection, collision.contacts[0].normal).normalized;
            if (reflectDirection.y < 0)
                reflectDirection = new Vector3(reflectDirection.x, -reflectDirection.y, reflectDirection.z);
            reflectDirection.y /= 100;

            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            //collision.gameObject.GetComponent<Animator>().enabled = false;
            //collision.gameObject.GetComponent<PlayerController>().enabled = false;
            playerRigidbody.AddForce(reflectDirection * _bounceForce, ForceMode.VelocityChange);


            // player lagdoll
            //collision.gameObject.GetComponent<PlayerRagdollController>().SetRagdollState(true);

            //Debug.Log(reflectDirection * _bounceForce);
        }
    }
}
