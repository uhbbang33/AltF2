using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTrigger : MonoBehaviour
{
    [SerializeField] GameObject _sharkObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _sharkObject.SetActive(true);

            //StartCoroutine(DestroyShark());
        }
    }

    IEnumerator DestroyShark()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(_sharkObject);
    }
}
