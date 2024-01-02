using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishObstacle : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _flapForce = 100f;
    private float _flapTime = 0.6f;

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
    }

    private void OnDisable()
    {
        StopCoroutine(Flap());
    }

    IEnumerator Flap()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            _rigidbody.AddForce(Vector3.up * _flapForce);
            _rigidbody.AddTorque(Vector3.forward * Random.Range(-1.0f, 1.0f) * 100f);

            yield return new WaitForSeconds(_flapTime);
        }
    }

}
