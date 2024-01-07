using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFooting : MonoBehaviour
{

    public float maintenanceTime;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BoxMaintenanceTime());
        }    
    }

    IEnumerator BoxMaintenanceTime()
    {

        yield return new WaitForSeconds(maintenanceTime);

        _animator.SetBool("IsStep", true);

        yield return new WaitForSeconds(maintenanceTime);

        gameObject.SetActive(false);

    }
}
