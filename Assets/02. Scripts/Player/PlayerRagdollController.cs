using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Collider _playerCollider;
    private Animator _animator;

    [SerializeField] private Rigidbody[] _lagdollRigidbodies;
    [SerializeField] private Collider[] _lagdollColliders;

    Coroutine _co;

    public GameObject tempObject;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        SetRagdollState(false);
    }

    public void SetRagdollState(bool state)
    {
        _playerRigidbody.isKinematic = state;
        _playerCollider.enabled = !state;
        _animator.enabled = !state;

        foreach(Rigidbody rb in _lagdollRigidbodies)
        {
            rb.isKinematic = !state;
        }
        foreach(Collider col in _lagdollColliders)
        {
            col.enabled = state;
        }

        if (state && _co == null)
        {
            _co = StartCoroutine(ReleaseRagdoll());
        }
    }

    IEnumerator ReleaseRagdoll()
    {
        //start
        yield return new WaitForSeconds(5f);
        _playerRigidbody.isKinematic = false;
        _playerCollider.enabled = true;
        _animator.enabled = true;

        foreach (Rigidbody rb in _lagdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in _lagdollColliders)
        {
            col.enabled = false;
        }

        transform.position = tempObject.transform.position;
        _co = null;
        //end
    }
}
