using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class FishObstacle : BaseObstacle
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _flapForce = 100f;
    [SerializeField] private float _torqueForce = 300f;
    [SerializeField] private float _flapTime = 0.6f;

    private IObjectPool<FishObstacle> _managedPool;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();

        float randomX = Random.Range(-1.0f, 1.0f);
        float randomZ = Random.Range(-1.0f, 1.0f);
        _rigidbody.AddForce(randomX * _jumpForce, 1.0f * _jumpForce, randomZ * _jumpForce);

        StartCoroutine(Flap());
        StartCoroutine(DestroyFish());
    }

    private void OnDisable()
    {
        StopCoroutine(Flap());
        StopCoroutine(DestroyFish());
    }

    IEnumerator Flap()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            _rigidbody.AddForce(Vector3.up * _flapForce);
            _rigidbody.AddTorque(Vector3.forward * Random.Range(-1.0f, 1.0f) * _torqueForce);

            yield return new WaitForSeconds(_flapTime);
        }
    }

    IEnumerator DestroyFish()
    {
        yield return new WaitForSeconds(20f);
        _managedPool.Release(this);
    }

    public void SetManagedPool(IObjectPool<FishObstacle> pool)
    {
        _managedPool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}