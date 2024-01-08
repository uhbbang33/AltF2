using System.Collections;
using UnityEngine;

public class SaveBoard : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
     }

    private void Start()
    {
        _rigidbody.velocity= Vector3.up * 3;

        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(4f);
        _rigidbody.AddForce(Vector3.down);
        _rigidbody.useGravity = true;
    }

}
