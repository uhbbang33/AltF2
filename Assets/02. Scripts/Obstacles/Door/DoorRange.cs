using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRange : MonoBehaviour
{
    private DoorObjectInteraction _doorObjectInteraction;

    private float _angleErrorRange = 1.0f;
    private Quaternion _objectRotation;

    private void Start()
    {
        _doorObjectInteraction = GetComponentInParent<DoorObjectInteraction>();
        _objectRotation = _doorObjectInteraction.transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            float angleDifference = Quaternion.Angle(_doorObjectInteraction.transform.rotation, _objectRotation);

            _doorObjectInteraction.DoorPushForce();

            if (angleDifference < _angleErrorRange)
            StartCoroutine(_doorObjectInteraction.InteractDoor());

        }
    }
}
