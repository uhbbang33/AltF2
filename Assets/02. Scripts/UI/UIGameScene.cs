using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameScene : MonoBehaviour
{
    public UIScore Score { get; private set; }

    private void Awake()
    {
        Score = GetComponentInChildren<UIScore>();
    }
}
