using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGear : MonoBehaviour
{
    [field : SerializeField] public Reciprocation Reciprocation { get; private set; }

    public string prefabName = "Gear";
    [field: SerializeField] public GameObject Gear { get; private set; }

    private void Awake()
    {
        if (Gear == null)
        {
            Gear = Instantiate(Resources.Load<GameObject>(prefabName), transform);
        }

        Reciprocation.SetStartTransform(transform);
        Reciprocation.SetTarget(Gear);
    }

    private void Update()
    {
        Reciprocation.Update();
    }

    private void OnValidate()
    {
        if(Gear != null)
        {
            Reciprocation.end.position = transform.position + Vector3.right * Reciprocation.Distance;
        }
    }
}
