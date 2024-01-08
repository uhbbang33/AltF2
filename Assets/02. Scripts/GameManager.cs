using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    #region Singleton Setting 
    private static GameManager _instanace;
    public static GameManager Instance
    { 
        get
        {
            _instanace = GameObject.Find("@GameManager")?.GetComponent<GameManager>();

            if(_instanace == null)
            {
                var go = new GameObject("@GameManager");
                _instanace = go.AddComponent<GameManager>();
                DontDestroyOnLoad(_instanace);
            }
            else
            {
                DontDestroyOnLoad(_instanace);
            }
            return _instanace;
        }
    }

    private readonly UIManager _ui = new UIManager();
    public static UIManager UI => Instance._ui;

    private readonly AudioManager _sound= new AudioManager();
    public static AudioManager Sound => Instance._sound;

    private GameManager()
    {

    }
    #endregion

    public int DeathCount { get; private set; } = 0;
    public float PlayTime { get; private set; } = 0;
    private UIScore _scoreUI;
    public bool IsPlayerDied { get; private set; } = true;
    private bool _isGameStarted = false;
    public PlayerController Player { get; private set; }

    private void Awake()
    {
        _ui.Init();
        _sound.Init();
        FindScoreUI();
    }

    private void Update()
    {
        if (_isGameStarted == false) return;
        PlayTime += Time.deltaTime;
        _scoreUI?.SetPlayTime(PlayTime);
    }

    private void OnLevelWasLoaded(int level)
    {
        IsPlayerDied = false;
        FindScoreUI();
    }


    private void FindScoreUI()
    {
        _scoreUI = GameObject.Find("HUD")?.GetComponent<UIGameScene>().Score;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        _isGameStarted = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
        var hpSystem = Player.GetComponent<HealthSystem>();
        var savePoint = Player.GetComponent<SavePoint>();

        if (hpSystem != null )
        {
            //hpSystem.OnHit += 
            hpSystem.OnDied += OnPlayerDied;
        }

        if(savePoint != null)
        {
            savePoint.OnRespawn += (() => { IsPlayerDied = false; });
        }
    }

    private void OnPlayerDied()
    {
        ++DeathCount;
        _scoreUI?.SetDeathCount(DeathCount);
        IsPlayerDied = true;
        _ui.ShowPoppUI(EPopup.UIDeath);
    }
}
