using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public string NextSceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneLoadManager.Instance.ChangeScene(NextSceneName);
        }
    }
}
