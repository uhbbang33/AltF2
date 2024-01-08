using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SharkObject : BounceObstacle
{
    private Rigidbody _rigidbody;
    private GameObject _player;
    private Vector3 _forceDirection = new();
    private Vector3 _originPos;
    private Quaternion _originRot;

    [SerializeField] GameObject _parent;
    [SerializeField] GameObject _targetPos;
    [SerializeField] float _jumpForce = 16f;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
        _originPos = transform.position;
        _originRot = transform.rotation;
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
        _rigidbody.velocity = _forceDirection * 14f + Vector3.up * _jumpForce;
        //_rigidbody.AddForce(_forceDirection * 800f + Vector3.up * 600f);
    }

    public void SetShark()
    {
        transform.SetParent(_parent.transform);

        transform.position = _originPos;
        transform.rotation = _originRot;

        gameObject.SetActive(false);
    }
}
