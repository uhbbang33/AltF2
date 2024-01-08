using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _startPoint; // 시작위치 설정.
    private Vector3 _firstStartPoint = new Vector3(0,50,50); // 1스테이지 시작위치 설정.
    private Vector3 _SecondStartPoint = new Vector3(94, 0, 15); // 2스테이지 시작위치 설정.

    private Vector3 _savePoint = Vector3.zero;  // 저장위치 설정.

    public event Action OnRespawn;

    private HealthSystem HealthSystem;
    private PlayerRagdollController _playerRagdollController;

    private void Awake()
    {
        _playerRagdollController = GetComponent<PlayerRagdollController>();
        HealthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneCheck(scene);
        _savePoint = _startPoint;
        HealthSystem.OnDied += Receive;
        //SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    public void Receive() 
    {
        StartCoroutine(ReStartCo());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            _playerRagdollController.SetRagdollState(true);
            StartCoroutine(ReStartCo());
        }
        checkSaveBoard();
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.3f);
    }

    private void checkSaveBoard() 
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 1,4))
        {
            if (_hit.transform.CompareTag("SaveBoard"))
            {
                _savePoint = _hit.transform.position + Vector3.up;
                //세이브 이펙트
                Destroy(_hit.collider.gameObject, 1f);
            }
        }
    }
    
    public IEnumerator ReStartCo()
    {
        yield return new WaitForSeconds(5.1f);

        PlayerRagdollController playerRagdoll = GetComponent<PlayerRagdollController>();
        playerRagdoll.SetRagdollState(false);
        playerRagdoll.ReturnPlayerPositionAndVelocity();

        SharkObject shark = GetComponentInChildren<SharkObject>();
        if (shark != null)
        {
            Destroy(shark.gameObject);
        }

        gameObject.transform.position = _savePoint;
        _savePoint = _startPoint;
        OnRespawn?.Invoke();
    }

    private void sceneCheck(Scene scene) 
    {
        if (scene.name == "KDH_Obstacle")
        {
            _startPoint = _firstStartPoint;
        }
        else if (scene.name == "99.BJH")
        {
            _startPoint = _SecondStartPoint;
        }
        else
        {
            _startPoint = Vector3.zero;
        }
    }
}
