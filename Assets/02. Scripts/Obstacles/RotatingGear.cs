using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGear : MonoBehaviour
{
    [field : SerializeField] public Arm Arm { get; private set; }

    public string prefabName = "Gear";
    [field: SerializeField] public GameObject Gear { get; private set; }

    private void Awake()
    {   
        if (Gear == null)
        {
            Gear = Instantiate(Resources.Load<GameObject>(prefabName), transform);
        }
    }

    private void OnValidate()
    {
        if (Gear != null)
        {
            Gear.transform.localPosition = Arm.offset;
        }
    }
}
