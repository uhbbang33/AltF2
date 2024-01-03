using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveBalloon : MonoBehaviour
{
    [Header("Speed")]
    public float speed;

    [Header("DestroyedObject")]
    public GameObject destroyedObject;
    
    private Animator _animator;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = Vector3.up * speed;

        StartCoroutine(TestPop());
    }

    IEnumerator TestPop()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(Pop());
        yield return null;
    }

    IEnumerator Pop()
    {
        _animator.SetTrigger("Pop");
        yield return new WaitForSeconds(1f);
        Destroy(destroyedObject);

        _rigidbody.useGravity = true;
        SavePosition();
    }

    private void SavePosition()
    {
        // position ¿˙¿Â
    }
}
