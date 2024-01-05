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

    private void Update()
    {
        if (GameManager.Instance.IsPlayerDied) return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                GameManager.UI.ClosePopupUI();
            }
            else
            {
                GameManager.UI.ShowPoppUI(EPopup.UIPauseGame);
            }
        }
    }
}
