using System.Collections;
using UnityEngine;


public class RotationJump : MonoBehaviour
{

    private float _rotateSpeed = 1f;
    private Vector3 _stopRotate = new Vector3(80, 0, 0);
    private Vector3 _startRotate = new Vector3(0, 0, 0);
    private bool _checkRotate = true;
    private bool _collidertime = true;

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Player"&&_checkRotate && _collidertime) 
        {
            StartCoroutine(RotatePad(collision));
            _collidertime = false;
        }
    }

    IEnumerator RotatePad(Collision collision)
    {
        while (_checkRotate)
        {
            _rotateSpeed += 0.01f;
            transform.Rotate(Vector2.right* _rotateSpeed);
            yield return null;
            if (_stopRotate.x - transform.eulerAngles.x <= 50f)
            {
                Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 forwardDirection = collision.transform.up; 
                if (otherRigidbody != null)
                {
                    otherRigidbody.AddForce(forwardDirection*10, ForceMode.Impulse);
                }

                transform.rotation = Quaternion.Euler(_stopRotate);
                _checkRotate =false;
            }
        }

        yield return new WaitForSeconds(2f) ;

        while (!_checkRotate)
        {
            transform.Rotate(Vector2.left);
            yield return null;

            if (transform.eulerAngles.x - _startRotate.x <= 1.1f)
            {
                transform.rotation = Quaternion.Euler(_startRotate);
                _checkRotate = true;
            }
        }
        _collidertime = true;
        _rotateSpeed = 1f;
    }
}
