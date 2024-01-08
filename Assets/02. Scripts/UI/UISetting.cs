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
        var box = transform.Find("Panel").Find("Volumes");

        var names = Enum.GetNames(typeof(Sliders));
        for(int i = 0; i < names.Length; ++i)
        {
            var slider = box.Find(names[i]).GetComponentInChildren<Slider>();
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
            slider.value = GameManager.Sound.MasterVolume;
            slider.onValueChanged.AddListener((x) => { OnMasterVolumeChanged(x);  });
        }

        if (_soundSliders.TryGetValue(Sliders.BGM.ToString(), out slider))
        {
            slider.value = GameManager.Sound.BGMVolume;
            slider.onValueChanged.AddListener((x) => { OnBGMVolumeChanged(x); });
        }

        if (_soundSliders.TryGetValue(Sliders.SFX.ToString(), out slider))
        {
            slider.value = GameManager.Sound.SFXVolume;
            slider.onValueChanged.AddListener((x) => { OnSFXVolumeChanged(x); });
        }        
    }

    private void OnMasterVolumeChanged(float volmue)
    {
        GameManager.Sound.MasterVolume = volmue;
    }

    private void OnBGMVolumeChanged(float volmue)
    {
        GameManager.Sound.BGMVolume = volmue;
    }

    private void OnSFXVolumeChanged(float volmue)
    {
        GameManager.Sound.SFXVolume = volmue;
    }
}
