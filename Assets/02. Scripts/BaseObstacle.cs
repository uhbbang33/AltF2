using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{

    private float _hitCount = 0;
    string name = "";

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player damage
            if (_hitCount < 3)
            {
                ParticleEffectManager.Instance.PlayFirstBloodParticle();
                _hitCount++;
                Debug.Log(_hitCount);
            }

            var player = collision.gameObject;
            player.GetComponent<HealthSystem>()?.Hit();
            

            // player lagdoll
            collision.gameObject.GetComponent<PlayerRagdollController>()?.SetRagdollState(true);

            nameset();

            GameManager.Sound.SFXPlay((name), gameObject.transform.position, 0.1f);
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
