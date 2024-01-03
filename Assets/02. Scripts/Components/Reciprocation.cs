using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Reciprocation
{
    private Transform _start;
    public Transform end;
    private Transform _destination;
    public GameObject Target { get; private set; }
    
    public float speed;
    private Vector3 _direction = Vector3.right;
    [field:SerializeField] public float Distance { get; private set; } = 0;


    public void SetStartTransform(Transform start)
    {
        _start = start;
        _destination = end;
    }

    public void Update()
    {
        if(ArriveDestination())
        {
            ChangeDirection();
        }
        Target.transform.Translate(_direction * Time.deltaTime * speed);
    }

    private bool ArriveDestination()
    {
        return Vector3.Distance(Target.transform.position, _destination.position) < 0.1f;
    }

    private void ChangeDirection()
    {
        _direction = _direction == Vector3.right ? Vector3.left : Vector3.right;
        _destination = _destination == _start ? end : _start;
    }

    public void SetTarget(GameObject go)
    {
        Target = go;
    }
}
