using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotatingThornyLog : MonoBehaviour
{
    public string bladePrefabName = "ThornyLog";
    [field :SerializeField] public GameObject Blade { get; private set; }

    [field :SerializeField] private BladeCountController[] _bladeCountControls;

    public BladeCountController.BladeCount bladeCount;

    [Range(1, 3)] public float length;

    public float offset;
    public float angle;

    private void Awake()
    {
        _bladeCountControls = GetComponentsInChildren<BladeCountController>();
    }

    private void OnValidate()
    {
        var log = transform.Find("Log").gameObject;
        log.transform.localScale = new Vector3(length, log.transform.localScale.y, log.transform.localScale.z);

        if (_bladeCountControls.Length != 0 && Blade != null) 
        { 
            foreach(var bladeCountControl in _bladeCountControls)
            {
                bladeCountControl.count = bladeCount;
                bladeCountControl.OnPreview(Blade, offset, angle);
                bladeCountControl.gameObject.SetActive(Math.Abs(bladeCountControl.transform.localPosition.x) < (int)Math.Floor(length * 2)); 
            }
        }
    }
}
