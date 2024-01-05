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
            Debug.Log(gameObject.gameObject.name);
            GMTest.Instance.audioManager.SFXPlay(gameObject.name.Replace("(Clone)", ""), gameObject.transform.position, 0.1f);
        }
    }
}
