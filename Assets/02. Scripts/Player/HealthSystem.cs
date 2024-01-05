using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _maxHeatlh = 3;
    private int _health = 0;
    public LayerMask deathLayer;

    public event Action OnHit;
    public event Action OnDied;

    private void Awake()
    {
        Reset();
    }

    private void Start()
    {
        // Player On Respawned += Reset();
    }

    public void Hit()
    {
        --_health;        

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

    private void Die()
    {
        _health = 0;
        OnDied?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == deathLayer)
        {
            Die();
        }
    }
}
