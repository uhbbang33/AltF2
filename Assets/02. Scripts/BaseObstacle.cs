using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    private float _hitCount = 0;
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player damage
            if(_hitCount < 3)
            {
              ParticleEffectManager.Instance.PlayBloodParticle();
               _hitCount++;
                Debug.Log(_hitCount);
            }

            // player lagdoll
            collision.gameObject.GetComponent<PlayerRagdollController>().SetRagdollState(true);

            Debug.Log(collision.gameObject.name);
        }
    }
}
