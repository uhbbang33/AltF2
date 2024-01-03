using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    [SerializeField]
    private bool _isRight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(MoveWall());
        }
    }

    IEnumerator MoveWall()
    {

        if (_isRight)
        {
            while (gameObject.transform.localPosition.x < 4.5f)
            {
                gameObject.transform.position += Vector3.right * 0.01f;
                yield return null;
            }
        }
        else if (!_isRight)
        {
            while (gameObject.transform.localPosition.x > 1.5f)
            {
                gameObject.transform.position += Vector3.left * 0.01f;
                yield return null;
            }
        }
    }
}
