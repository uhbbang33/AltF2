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
    private GameObject _bladePrefab;
    private float _offset;

    private void Awake()
    {
        DestroyBlades();
        CreateBlades();
        RotateBlades();
    }

    public void OnPreview(GameObject prefab, float offset)
    {
        _bladePrefab = prefab;
        _offset = offset;

        for (int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            _blades[i].GetComponent<MeshFilter>().mesh = prefab.GetComponent<MeshFilter>()?.sharedMesh;
            _blades[i].transform.localPosition = positions[i] * offset;
        }

        ActiveBlades();
    }

    private void RotateBlades()
    {
        for (int i = 0; i < (int)BladeCount.QUAD; ++i)
        {
            if (i != 3)
            {
                var rot = Vector3.Cross(_blades[i].transform.forward, positions[i]);
                _blades[i].transform.rotation = Quaternion.Euler(rot * 90);
            }
            else
            {
                _blades[i].transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
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
