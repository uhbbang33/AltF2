using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    enum Sliders
    {
        Master,
        BGM,
        SFX,    
    }

    private Dictionary<string, Slider> _soundSliders= new Dictionary<string, Slider>();

    private void Awake()
    {
        var box = transform.Find("Panel").Find("GridBox");

        var names = Enum.GetNames(typeof(Sliders));
        for(int i = 0; i < names.Length; ++i)
        {
            var slider = box.Find(names[i]).GetComponent<Slider>();
            if(slider != null)
            {
                _soundSliders.TryAdd(names[i], slider);
            }
        }
    }

    private void Start()
    {
        Slider slider;
        if (_soundSliders.TryGetValue(Sliders.Master.ToString(), out slider))
        {
            // 기본으로 설정 되어있는 사운드 값 설정하기
            slider.onValueChanged.AddListener((x) => { OnMasterVolumeChanged(x);  });
        }

        if (_soundSliders.TryGetValue(Sliders.BGM.ToString(), out slider))
        {
            // 기본으로 설정 되어있는 사운드 값 설정하기
            slider.onValueChanged.AddListener((x) => { OnBGMVolumeChanged(x); });
        }

        if (_soundSliders.TryGetValue(Sliders.SFX.ToString(), out slider))
        {
            // 기본으로 설정 되어있는 사운드 값 설정하기
            slider.onValueChanged.AddListener((x) => { OnSFXVolumeChanged(x); });
        }        
    }

    private void OnMasterVolumeChanged(float volmue)
    {
        // 사운드 관리하는 클래스에 값 전달하는 코드
    }

    private void OnBGMVolumeChanged(float volmue)
    {
        // 사운드 관리하는 클래스에 값 전달하는 코드
    }

    private void OnSFXVolumeChanged(float volmue)
    {
        // 사운드 관리하는 클래스에 값 전달하는 코드
    }
}
