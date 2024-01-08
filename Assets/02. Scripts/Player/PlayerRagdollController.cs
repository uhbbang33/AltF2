using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Collider _playerCollider;
    private Animator _animator;
    private PlayerController _playerController;

    [SerializeField] private Rigidbody[] _lagdollRigidbodies;
    [SerializeField] private Collider[] _lagdollColliders;

    [SerializeField] public GameObject _pevisObject;
    [SerializeField] private GameObject _cameraRoot;

    Coroutine _co;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();

        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += SetRagdollStateTrue;

        SetRagdollState(false);
    }

    public void SetRagdollState(bool state)
    {
        _playerCollider.enabled = !state;
        _animator.enabled = !state;

        foreach (Rigidbody rb in _lagdollRigidbodies)
        {
            rb.isKinematic = !state;
        }
        foreach (Collider col in _lagdollColliders)
        {
            col.enabled = state;
        }

        if (state && _co == null)
        {
            _cameraRoot.transform.SetParent(_pevisObject.transform, false);
            _co = StartCoroutine(AfterRagdollState());
        }
    }

    IEnumerator AfterRagdollState()
    {
        _playerController.InputActionLocked();

        yield return new WaitForSeconds(5f);
        

        // IsPlayerDied == false?
        if (!GameManager.Instance.IsPlayerDied)
        {
            SetRagdollState(false);
        }
        ReturnPlayerPositionAndVelocity();

        _cameraRoot.transform.SetParent(transform, false);
        _co = null;

        _playerController.InputActionUnLocked();
    }

    public void ReturnPlayerPositionAndVelocity()
    {
        _playerRigidbody.velocity = Vector3.zero;
        transform.position = _pevisObject.transform.position + Vector3.up;
    }

    // ?
    private void SetRagdollStateTrue()
    {
        SetRagdollState(true);
    }

    public void AddForceToPevis(Vector2 force)
    {
        _pevisObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
    }
}
