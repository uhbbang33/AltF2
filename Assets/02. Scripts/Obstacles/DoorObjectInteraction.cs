using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorObjectInteraction : BounceObstacle
{
    [Header("- Rot")]
    [SerializeField] private float rotX;
    [SerializeField] private float rotY;
    [SerializeField] private float rotZ;

    [Header("- time")]
    [SerializeField] private float waitTime;

    [Header("- speed")]
    [SerializeField] private float duration;  // 회전에 걸리는 시간

    [Header("- force")]
    [SerializeField] private float addForce;

    private Rigidbody _rigidBody;
    private GameObject _gameObject;

   private float angleErrorRange = 1.0f;

    private Quaternion _objectRotation;
    private bool _isInteracting = false;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _gameObject = _rigidBody.gameObject;
        _objectRotation = gameObject.transform.rotation;

        Debug.Log(_objectRotation.y);
        Debug.Log(gameObject.transform.rotation.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Player")) //&& Quaternion.Equals(_gameObject.transform.rotation, _objectRotation)
        {
            float angleDifference = Quaternion.Angle(_gameObject.transform.rotation, _objectRotation);

            //StartCoroutine(PlayerComponentControl(collision));

            DoorPushForce();

            if (angleDifference < angleErrorRange)
            {
                Debug.Log("???");
                // 여기에 문을 열기 위한 로직 추가
                StartCoroutine(InteractDoor());
            }
            //InteractDoor();
            //StartCoroutine(InteractDoor());
        }
    }


    /// <summary>
    /// 코루틴을 이용해 문을 회전
    /// </summary>
    /// <returns></returns>
    private IEnumerator InteractDoor()
    {
        _isInteracting = true;

        Quaternion currentRotation = _gameObject.transform.rotation; // 오브젝트의 현재 rotaion값
        
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(rotX, rotY, rotZ)); // 내가 원하는 rotation값

        float elapsedTime = 0f;

        
        
        // 회전 시간
        while (elapsedTime < duration)
        {
            // Quaternion.Lerp 쿼터니언의 중간의 값을 반환하여 부드러운 효과를 줌, Quaternion.Lerp(시작 쿼터니언, 타겟 쿼터니언, 0~1까지 있는 시간)
            _gameObject.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _gameObject.transform.rotation = targetRotation;


        /// 2초간 대기
        yield return new WaitForSeconds(waitTime);  

        // 역회전
        Quaternion reverseRotation = Quaternion.Euler(targetRotation.eulerAngles + new Vector3(rotX, -rotY, rotZ));
        elapsedTime = 0f;

        // 역회전 시간
        while (elapsedTime < duration)
        {
            _gameObject.transform.rotation = Quaternion.Lerp(targetRotation, reverseRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gameObject.transform.rotation = _objectRotation;  // 초기 위치로 돌아옴
        _isInteracting = false;  // 상호작용 종료
    }


    private void DoorPushForce()
    {
        float zValue = 1f;

        Vector3 forceDirection = _gameObject.transform.forward;
        forceDirection.x = 0;
        forceDirection.z = zValue;

        _rigidBody.AddForce(forceDirection.normalized * addForce, ForceMode.Impulse);
        Debug.Log(forceDirection.normalized * addForce);

    }


    //private IEnumerator PlayerComponentControl(Collision collision)
    //{
    //    collision.gameObject.GetComponent<Animator>().enabled = false;
    //    collision.gameObject.GetComponent<PlayerController>().enabled = false;

    //    yield return new WaitForSeconds(1.1f);

    //    collision.gameObject.GetComponent<Animator>().enabled = true;
    //    collision.gameObject.GetComponent<PlayerController>().enabled = true;

    //}

}
