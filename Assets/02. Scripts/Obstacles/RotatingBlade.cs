using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotatingBlade : MonoBehaviour
{
    public string prefabName = "Blade";
    [field : SerializeField] public GameObject Blade { get; private set; }
    public BladeCountController BladeCountController { get; private set; }

    public BladeCountController.BladeCount bladeCount = BladeCountController.BladeCount.ONLY;

    public float offset;
    public float angle;

    private void Awake()
    {
        if(Blade == null)
        {
            Blade = Resources.Load<GameObject>(prefabName);
        }

        BladeCountController = GetComponentInChildren<BladeCountController>();
    }

    private void OnValidate()
    {
        var bladeController = GetComponentInChildren<BladeCountController>();
        
        if (bladeController != null)
        {
            bladeController.count = bladeCount;
        }

        if (Blade != null)
        {
            bladeController.OnPreview(Blade, offset, angle);
        }
    }
}
