using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BladeCountController : MonoBehaviour
{
    public enum BladeCount
    {
        ONLY = 1,
        DOUBLE = 2,
        QUAD = 4
    }

    public BladeCount count = BladeCount.ONLY;
    [SerializeField] private GameObject[] _blades = new GameObject[(int)BladeCount.QUAD];

    private Vector3[] positions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
    private int[] _zAngle = { 90, -90, 0, 180 };
    private GameObject _bladePrefab;
    private float _offset;
    private float _angle;

    private void Awake()
    {
        DestroyBlades();
        CreateBlades();
        RotateBlades();
    }

    public void OnPreview(GameObject prefab, float offset, float angle)
    {
        _bladePrefab = prefab;
        _offset = offset;
        _angle = angle;

        for (int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            _blades[i].GetComponent<MeshFilter>().mesh = prefab.GetComponent<MeshFilter>()?.sharedMesh;
            _blades[i].transform.localPosition = positions[i] * offset;
            _blades[i].transform.localScale = prefab.transform.localScale;
        }

        RotateBlades();
        ActiveBlades();
    }

    private void RotateBlades()
    {
        for (int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            _blades[i].transform.localRotation = Quaternion.Euler(new Vector3(0, _zAngle[i], 0) + Vector3.right * _angle);
        }
    }

    private void CreateBlades()
    {
        for(int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            _blades[i] = Instantiate(_bladePrefab, transform);
            _blades[i].transform.localPosition = positions[i] * _offset;
        }
    }

    private void DestroyBlades()
    {
        foreach(var blade in _blades)
        {
            Destroy(blade);
        }
    }

    private void ActiveBlades()
    {
        for(int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            _blades[i].SetActive(i < (int)count);
        }
    }
}
