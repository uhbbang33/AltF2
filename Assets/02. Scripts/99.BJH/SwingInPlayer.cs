using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingInPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private ThirdPersonController _controller;

    private void Start()
    {
        _controller = _player.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        //if(!_controller.Grounded) 
        //{
        //    _controller.gameObject.transform.SetParent(null);
        //}
    }

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
