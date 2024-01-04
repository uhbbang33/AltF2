using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지

            
        }
    }
}
