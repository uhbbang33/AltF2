using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    string name = "";
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player damage
            var player = collision.gameObject;
            player.GetComponent<HealthSystem>()?.Hit();
            

            // player lagdoll
            collision.gameObject.GetComponent<PlayerRagdollController>()?.SetRagdollState(true);

            nameset();

            GMTest.Instance.audioManager.SFXPlay((name), gameObject.transform.position, 0.1f);
        }
    }

    private void nameset() 
    {
        name = gameObject.name.Replace("(Clone)", "");
        for (int i = 0; i < 20; i++)
        {
            name = name.Replace(" (" + i + ")", "");
        }
    }
}
