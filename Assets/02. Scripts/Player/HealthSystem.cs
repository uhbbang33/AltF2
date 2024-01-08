using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _maxHeatlh = 3;
    private int _health = 0;
    public LayerMask deathLayer;

    private SavePoint _savePoint;
    private RagdollCollisionWithSea _ragdollcollision;

    public event Action OnHit;
    public event Action OnDied;

    public GameObject firsHitBloodParticle;
    public GameObject secondHitBloodParticle;

    private void Awake()
    {
        Reset();
        _savePoint = GetComponent<SavePoint>();
        _ragdollcollision = GetComponentInChildren<RagdollCollisionWithSea>();
    }

    private void Start()
    {
        _savePoint.OnRespawn += OnRespawned;
        _ragdollcollision.OnDieInSea += Die;
        OnDied += Reset;
    }

    public void Hit()
    {
        --_health;
        HitParticleEvent(_health);

        if(_health == 0)
        {
            OnDied?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    private void Reset()
    {
        _health = _maxHeatlh;
    }

    private void OnRespawned()
    {
        GameManager.UI.ClosePopupUI();
        ResetParticle();
    }

    private void Die()
    {
        OnDied?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer | deathLayer) == deathLayer)
        {
            ParticleEffectManager.Instance.PlaySeaParticle(transform.position);
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer | deathLayer) == deathLayer)
        {
            ParticleEffectManager.Instance.PlaySeaParticle(transform.position);
            Die();
        }
    }

    private void HitParticleEvent(int _curHealth)
    {
        if(_curHealth == 2)
        {
            Debug.Log("HIT!");
            //ParticleEffectManager.Instance.PlayFirstBloodParticle();
            ParticleEffectManager.Instance.PlayBloodParticle(firsHitBloodParticle);
        }
        else if(_curHealth == 1)
        {
            //ParticleEffectManager.Instance.PlaySecondBloodParticle();
            ParticleEffectManager.Instance.PlayBloodParticle(secondHitBloodParticle);
        }
    }

    private void ResetParticle()
    {
        ParticleEffectManager.Instance.ResetParticle(firsHitBloodParticle);
        ParticleEffectManager.Instance.ResetParticle(secondHitBloodParticle);
    }

}
