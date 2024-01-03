using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingThornyLog : MonoBehaviour
{
    public string bladePrefabName = "ThornyLog";
    [field :SerializeField] public GameObject Blade { get; private set; }

    private BladeCountController[] _bladeCountControls;

    public BladeCountController.BladeCount bladeCount;

    public float offset;
    public float angle;

    private void Awake()
    {
        _bladeCountControls = GetComponentsInChildren<BladeCountController>();
    }

    private void OnValidate()
    {
        var bladeCountControls = GetComponentsInChildren<BladeCountController>();
        if(bladeCountControls.Length != 0 && Blade != null) 
        { 
            foreach(var bladeCountControl in bladeCountControls)
            {
                bladeCountControl.count = bladeCount;
                bladeCountControl.OnPreview(Blade, offset, angle);
            }
        }
    }
}
