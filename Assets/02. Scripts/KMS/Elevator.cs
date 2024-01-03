using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Elevator : MonoBehaviour
{
    public float cycleTime;
    public float heightPos;
    public Vector3 direction;

    private bool isMovingForward = true;
    private float speed;

    [HideInInspector]
    public Vector3 vec;
    


    private void Start()
    {
        speed = heightPos / (cycleTime / 2);
        StartCoroutine(Movement(cycleTime));
    }

    private void FixedUpdate()
    {
        Move();
    }

    IEnumerator Movement(float cycleTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleTime / 2);
            isMovingForward = !isMovingForward;
        }
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        if (isMovingForward)
        {
            movement = direction * speed * Time.deltaTime;
        }
        else
        {
            movement = -direction * speed * Time.deltaTime;
        }

        transform.position += movement;

        // 부모-자식 관계에 있는 모든 자식 오브젝트를 이동 방향과 속도에 맞춰 이동
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Player")) // 플레이어 태그를 가진 자식만 이동
            {
                child.position += (vec - this.transform.position) * Time.deltaTime;
                vec = this.transform.position;
            }
        }
    }
}
