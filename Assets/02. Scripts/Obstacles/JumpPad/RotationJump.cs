using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class RotationJump : MonoBehaviour
{
    [SerializeField]
    private float _jumpforce = 40f;
    private float _rotateSpeed = 0.5f;
    private Vector3 _stopRotate = new Vector3(70, 0, 0);
    private bool _checkRotate = true;
    private bool _collidertime = true;
    private bool _onPad = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _checkRotate && _collidertime)
        {
            StartCoroutine(RotatePad(collision));
            _collidertime = false;
            _onPad = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _onPad = false;
    }

    IEnumerator RotatePad(Collision collision)
    {

        yield return new WaitForSeconds(0.2f);
        while (_checkRotate)
        {
            _rotateSpeed += 0.01f;
            transform.Rotate(Vector2.right * _rotateSpeed);
            yield return null;
            if (_stopRotate.x - transform.eulerAngles.x <= 10f)
            {

                Vector3 forwardDirection = transform.up;
                
                //Rigidbody Addforce  (y는 힘을 받는데 z 힘을 못받음)
                Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (otherRigidbody != null&& _onPad)
                {
                    StartCoroutine(PlayerEnabledf(collision)); // 임시
                    Debug.Log("addforce 방향 : " + forwardDirection * _jumpforce);
                    otherRigidbody.AddForce(forwardDirection * _jumpforce, ForceMode.Impulse);
                }

                
                _checkRotate = false;
            }
        }

        yield return new WaitForSeconds(2f);

        while (!_checkRotate)
        {
            transform.Rotate(Vector2.left);
            yield return null;

            if (transform.eulerAngles.x <= 1.1f)
            {
                _checkRotate = true;
            }
        }
        _collidertime = true;
        _rotateSpeed = 1f;
    }

    //나중에 고치기
    IEnumerator PlayerEnabledf(Collision collision) 
    {
        yield return null;
        collision.gameObject.GetComponent<Animator>().enabled = false;
        collision.gameObject.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(1.1f);
        collision.gameObject.GetComponent<Animator>().enabled = true;
        collision.gameObject.GetComponent<PlayerController>().enabled = true;
    }
}
