using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI _deathCount;
    private TextMeshProUGUI _playTime;

    private void Awake()
    {
        _deathCount = transform.Find("DeathCount").GetComponent<TextMeshProUGUI>();
        _playTime = transform.Find("PlayTime").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetDeathCount(GameManager.Instance.DeathCount);
    }

    public void SetDeathCount(int deathCount)
    {
        _deathCount.text = $"Death : {deathCount} th";
    }

    public void SetPlayTime(float time)
    {
        int hour = (int)(time / 3600);
        int min = (int)(time / 60 % 60);
        int sec = (int)(time % 60);
        _playTime.text = string.Format("Time {0:D2} : {1:D2} : {2:D2}", hour, min, sec);
    }
}
