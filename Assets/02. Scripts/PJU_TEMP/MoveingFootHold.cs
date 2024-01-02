using GearFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingFootHold : MonoBehaviour
{
    [field :SerializeField] public Reciprocation Reciprocation { get; private set; }
    public GameObject foothold;
    // Start is called before the first frame update
    void Start()
    {
        Reciprocation.SetStartTransform(transform);
        Reciprocation.SetTarget(foothold);
    }

    // Update is called once per frame
    void Update()
    {
        Reciprocation.Update();
    }
}
