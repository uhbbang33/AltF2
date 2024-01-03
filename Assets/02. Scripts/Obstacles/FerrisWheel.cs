using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheel : MonoBehaviour
{
    public Rotator Rotator { get; private set; }
    private Rotator[] _childrenRotators;

    private void Awake()
    {
        Rotator = GetComponent<Rotator>();
    }

    private void Start()
    {
        _childrenRotators = GetComponentsInChildren<Rotator>();
        foreach(var rotator in _childrenRotators)
        {
            rotator.speed = Rotator.speed * -1.0f;
            rotator.axis = Rotator.axis;
        }
    }
}
