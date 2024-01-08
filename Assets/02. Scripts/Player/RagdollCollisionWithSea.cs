using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCollisionWithSea : MonoBehaviour
{
    public event Action OnDieInSea;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sea"))
        {
            ParticleEffectManager.Instance.PlaySeaParticle(transform.position);
            OnDieInSea.Invoke();
        }
    }
}
