using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 2.0f;  // 판자의 흔들리는 속도
    [SerializeField] private float swingAngle = 30.0f; // 판자의 최대 흔들리는 각도

    void Update()
    {
        // swing
        float angle =  Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // 판자 rotation값 변화
        transform.rotation = Quaternion.Euler(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    }
}
