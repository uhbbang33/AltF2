using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SharkObject : BounceObstacle
{
    private Rigidbody _rigidbody;
    private GameObject _player;
    private Vector3 _forceDirection = new();

    [SerializeField] GameObject _targetPos;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");

        JumpToPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            //_player.GetComponent<PlayerRagdollController>()._pevisObject.transform.SetParent(transform);
            transform.SetParent(_player.GetComponent<PlayerRagdollController>()._pevisObject.transform);
            _rigidbody.isKinematic = true;
        }
    }

    private void JumpToPlayer()
    {
        _forceDirection = (_targetPos.transform.position - transform.position).normalized;
        _rigidbody.velocity = _forceDirection * 14f + Vector3.up * 16f;
        //_rigidbody.AddForce(_forceDirection * 800f + Vector3.up * 600f);
    }
}
