using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DoorObjectInteraction : MonoBehaviour
{
    [Header("- Rot")]
    [SerializeField] private float rotX;
    [SerializeField] private float rotY;
    [SerializeField] private float rotZ;

    [Header("- time")]
    [SerializeField] private float waitTime;

    private Rigidbody _rigidBody;
    private GameObject _gameObject;

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
        if (collision.gameObject.CompareTag("Player") && Quaternion.Equals(_gameObject.transform.rotation, _objectRotation))
        {
            Debug.Log("???");
            //InteractDoor();
            StartCoroutine(InteractDoor());

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
        
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(rotX, -rotY, rotZ)); // 내가 원하는 rotation값

        float elapsedTime = 0f;
        float duration = 0.3f; // 회전에 걸리는 시간

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
        Quaternion reverseRotation = Quaternion.Euler(targetRotation.eulerAngles + new Vector3(rotX, rotY, rotZ));
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

}
