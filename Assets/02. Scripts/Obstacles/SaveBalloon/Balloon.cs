using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private SaveBalloon _saveBalloon;

    [SerializeField]
    private LayerMask _projectileLayer;

    private void Start()
    {
        _saveBalloon = GetComponentInParent<SaveBalloon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_projectileLayer.value == (_projectileLayer.value | (1 << other.gameObject.layer))) {
            StartCoroutine(_saveBalloon.Pop(gameObject));
        }
    }
}
