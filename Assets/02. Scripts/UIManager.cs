using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager
{
    private int _order = 10;

    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    private Dictionary<string, GameObject> _popupUIs = new Dictionary<string, GameObject>();

    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@UI_Root");

            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
                Object.DontDestroyOnLoad(root);
            }
            else
            {
                Object.DontDestroyOnLoad(root);
            }

            return root;
        }
    }

    public void Init()
    {
        var prefabs = Resources.LoadAll<GameObject>("UI");
        foreach(var prefab in prefabs)
        {
            var ui = Object.Instantiate(prefab, Root.transform);
            ui.SetActive(false);
            _popupUIs.TryAdd(prefab.name, ui);
        }

        var eventSystem = GameObject.Find("@EventSystem");
        if (eventSystem == null)
        {
            eventSystem = Object.Instantiate(Resources.Load<GameObject>("@EventSystem"));
        }
        Object.DontDestroyOnLoad(eventSystem);
    }

    public void ShowPoppUI(EPopup type)
    {
        var name = type.ToString();

        if(_popupUIs.ContainsKey(name))
        {
            _popupUIs[name].GetComponent<Canvas>().sortingOrder = _order++;
            _popupStack.Push(_popupUIs[name]);
            Cursor.lockState = CursorLockMode.None;
            _popupUIs[name].SetActive(true);
        }
    }

    public void ClosePopupUI()
    {
        if(_popupStack.Count > 0 )
        {
            --_order;
            _popupStack.Pop().SetActive(false);
        }

        if(_popupStack.Count == 0 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
