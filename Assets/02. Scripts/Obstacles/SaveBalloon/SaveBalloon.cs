using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveBalloon : MonoBehaviour
{
    [Header("Speed")]
    public float speed;
    
    private Animator _animator;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
       // 시작하자마자 올라가는 부분 수정
    }

    public IEnumerator Pop(GameObject gameObj)
    {
        _animator.SetTrigger("Pop");
        yield return new WaitForSeconds(1f);

        _rigidbody.useGravity = true;
        SavePosition();

        Destroy(gameObj);
    }

    private void SavePosition()
    {
        // position 저장
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _rigidbody.velocity = Vector3.up * speed;
        }
    }

}
