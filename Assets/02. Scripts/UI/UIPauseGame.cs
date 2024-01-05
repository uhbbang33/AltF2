using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseGame : MonoBehaviour
{
    enum Buttons
    {
        Resume,
        Option,
        Exit
    }

    private Dictionary<string, Button> _buttons = new Dictionary<string, Button>();

    private void Awake()
    {
        var panel = transform.Find("Panel");

        var names = Enum.GetNames(typeof(Buttons));
        for(int i = 0; i <  names.Length; ++i)
        {
            var button = panel.Find(names[i]).GetComponent<Button>();
            if(button != null)
            {
                _buttons.TryAdd(names[i], button);
            }
        }        
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        Button button;
        if(_buttons.TryGetValue(Buttons.Resume.ToString(), out button))
        {
            button.onClick.AddListener(() => { GameManager.UI.ClosePopupUI(); });
        }

        if (_buttons.TryGetValue(Buttons.Option.ToString(), out button))
        {
            button.onClick.AddListener(() => { GameManager.UI.ShowPoppUI(EPopup.UISetting); });
        }

        if (_buttons.TryGetValue(Buttons.Exit.ToString(), out button))
        {
            button.onClick.AddListener(() => { GameManager.Instance.ExitGame(); });
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }
}
