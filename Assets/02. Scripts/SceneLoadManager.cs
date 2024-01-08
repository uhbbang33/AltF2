using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoadManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public Image LoadProgressBar;
    public float ScreenHideSpeed;

    public static SceneLoadManager Instance;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (FindObjectsOfType<SceneLoadManager>().Length  >= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = FindObjectOfType<SceneLoadManager>();
            DontDestroyOnLoad(gameObject);
        }

        _canvasGroup = LoadingCanvas.GetComponent<CanvasGroup>();
    }
    //string으로 씬 불러오기
    public void ChangeScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FillProgressBar(asyncLoad));
    }
    //int로 씬 불러오기
    public void ChangeScene(int sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FillProgressBar(asyncLoad));
    }

    IEnumerator FillProgressBar(AsyncOperation asyncLoad)
    {
        //로딩 화면 초기화
        LoadProgressBar.fillAmount = 0;

        LoadingCanvas.SetActive(true);
        _canvasGroup.alpha = 1.0f;

        //게이지 FillAmount
        while (LoadProgressBar.fillAmount <= 1)
        {
            LoadProgressBar.fillAmount = asyncLoad.progress;

            yield return new WaitForSecondsRealtime(0.3f);

            if (LoadProgressBar.fillAmount >= 1)
            {
                break;
            }
        }
        StartCoroutine(HideAlphaLoadingCanvas());
    }

    IEnumerator HideAlphaLoadingCanvas()
    {
        while (_canvasGroup.alpha != 0)
        {
            _canvasGroup.alpha -= 1 / ScreenHideSpeed * Time.deltaTime;
            yield return null;
        }
        LoadingCanvas.SetActive(false);
    }
}
