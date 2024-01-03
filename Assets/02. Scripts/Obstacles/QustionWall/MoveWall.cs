using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{

    private Rigidbody _wallRigidbody ;
    private bool _isMoving;
    private float _speed = 3f;

private void Awake()
    {
        _wallRigidbody=GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _isMoving = false;
        gameObject.SetActive(true);
        
        //transform .position = new Vector3(-11, 0.1f, 5);
    }
    private void Update()
    {
        if (_isMoving)
        {
            gameObject.transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            _isMoving = true;
            Invoke("hideWall", 10f);
        }
    }

    private void hideWall() 
    {
        gameObject.SetActive(false);
    }
}

