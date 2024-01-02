using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rotator :MonoBehaviour
{
    public float speed;

    public enum Axis
    {
        X,
        Y,
        Z,
    }

    private Vector3[] _axis = { Vector3.right , Vector3.up, Vector3.forward };
    public Axis axis = Axis.Z;

    public void Update()
    {
        transform.Rotate(_axis[(int)axis], speed);
    }
}
