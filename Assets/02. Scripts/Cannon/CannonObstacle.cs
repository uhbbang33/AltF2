using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonObstacle : MonoBehaviour
{

    [Header("Delay")]
    public float DelayTime;

    [Header("Ball Direction")]
    public GameObject OriginPositionObject;
    public GameObject TargetPositionObject;

    [Header("Ball Setting")]
    public GameObject Ball;
    public GameObject BallParent;
    public int InstanceCount;
    public float ShootForce;

    private Queue<GameObject> _ballQueue = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < InstanceCount; i++)
        {
            Instantiate(Ball, BallParent.transform);
        }
        for(int i = 0; i < BallParent.transform.childCount; i++)
        {
            _ballQueue.Enqueue(BallParent.transform.GetChild(i).gameObject);
        }
        
        StartCoroutine(InstanceBall());
    }
    
    private void ActiveBall(GameObject ball)
    {
        ball.transform.position = TargetPositionObject.transform.position;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().AddForce((TargetPositionObject.transform.position - OriginPositionObject.transform.position).normalized * ShootForce, ForceMode.Impulse);
        _ballQueue.Enqueue(ball);
    }

    private IEnumerator InstanceBall()
    {
        while (true)
        {
            ActiveBall(_ballQueue.Dequeue());
            yield return new WaitForSeconds(DelayTime);
        }
    }

}
