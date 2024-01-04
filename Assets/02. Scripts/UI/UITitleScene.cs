using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITitleScene : MonoBehaviour
{
    enum Buttons
    {
        Start,
        Setting,
        Exit,
    }

    private Dictionary<string, Button> _buttons = new Dictionary<string, Button>();

    private void Awake()
    {
        var box = transform.Find("GridBox");
        var names = Enum.GetNames(typeof(Buttons));
        for (int i = 0; i < names.Length; ++i)
        {
            var button = box.Find(names[i]).GetComponent<Button>();
            if (button != null)
            {
                _buttons.TryAdd(names[i], button);
            }
        }
    }

    private void Start()
    {
        Button button;
        if (_buttons.TryGetValue(Buttons.Start.ToString(), out button))
        {
            button.onClick.AddListener(() => { /* Load Game Scene */  });
        }

        if (_buttons.TryGetValue(Buttons.Setting.ToString(), out button))
        {
            button.onClick.AddListener(() => { GameManager.UI.ShowPoppUI(EPopup.UISetting); });
        }

        if (_buttons.TryGetValue(Buttons.Exit.ToString(), out button))
        {
            button.onClick.AddListener(() => { GameManager.Instance.ExitGame(); });
        }
    }
}
