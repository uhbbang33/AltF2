using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class RotationJump : MonoBehaviour
{
    [SerializeField]
    private float _jumpforce = 30f;
    private float _rotateSpeed = 0.5f;
    private Vector3 _stopRotate = new Vector3(60, 0, 0);
    private Vector3 _startRotate = new Vector3(0, 0, 0);
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
        PlayerJump(collision, Vector3.left);

        yield return new WaitForSeconds(0.1f);
        while (_checkRotate)
        {
            _rotateSpeed += 0.01f;
            transform.Rotate(Vector2.right * _rotateSpeed);
            yield return null;
            if (_stopRotate.x - transform.eulerAngles.x <= 10f)
            {

                Vector3 forwardDirection = transform.up;
                
                //Rigidbody Addforce 에서 다른 방법으로 변경 (y는 힘을 받는데 z 힘을 못받음)
                Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (otherRigidbody != null&& _onPad)
                {
                    //StartCoroutine(PlayerJump(collision, forwardDirection));
                    Debug.Log("addforce 방향 : " + forwardDirection * _jumpforce);
                    otherRigidbody.AddForce(forwardDirection * _jumpforce, ForceMode.Impulse);
                }

                transform.rotation = Quaternion.Euler(_stopRotate);
                _checkRotate = false;
            }
        }

        yield return new WaitForSeconds(2f);

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

    //억지로 밀기 다른 방법 생각 해보기.
    IEnumerator PlayerJump(Collision collision, Vector3 forwardDirection)
    {
        float time = 0;
        Debug.Log(time);

        while (time < 0.7f)
        {
            time += Time.deltaTime;
            collision.gameObject.transform.position += forwardDirection;
            yield return null;
            if (forwardDirection.y >0.01f && forwardDirection.z > 0.01f)
            {
                forwardDirection -= new Vector3(0,0.01f,0.01f);
                Debug.Log(forwardDirection);
            }

        }
        time = 0;
    }

}
