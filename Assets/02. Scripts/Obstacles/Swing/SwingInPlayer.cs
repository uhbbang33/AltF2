using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingInPlayer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            Debug.Log("HI");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("HELLO");
            collision.gameObject.transform.SetParent(null);
        }
    }

}
