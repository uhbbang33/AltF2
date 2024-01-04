using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player damage


            // player lagdoll
            collision.gameObject.GetComponent<PlayerRagdollController>().SetRagdollState(true);

            Debug.Log(collision.gameObject.name);
        }
    }
}
