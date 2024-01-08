using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChickenProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float offsetAngle;

    private const float waitTime = 0.1f;
    private const float destroyDelayTime = 3f;
    private Rigidbody _rigidbody;
    private Transform _mainCameraTransform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
        ParticleEffectManager.Instance.Playfeather(gameObject);
        StartCoroutine(Fire());

    }

    IEnumerator Fire()
    {
        var dir = GetCamDir();
        dir.y += offsetAngle;
        yield return new WaitForSeconds(waitTime);
        Rotate(dir);
        _rigidbody.velocity = dir * speed;
        Destroy(gameObject, destroyDelayTime);
    }

    private Vector3 GetCamDir()
    {
        var fowardDir = _mainCameraTransform.forward;
        fowardDir.Normalize();
        return fowardDir;
    }

    private void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌처리
        if (other.gameObject.CompareTag("Balloon"))
            Destroy(gameObject);
    }
}
