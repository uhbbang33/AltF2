using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BounceObstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
