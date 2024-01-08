using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _startPoint; // ������ġ ����.
    private Vector3 _firstStartPoint = new Vector3(0,51,31); // 1�������� ������ġ ����.
    private Vector3 _SecondStartPoint = new Vector3(94, 1, 15); // 2�������� ������ġ ����.

    private Vector3 _savePoint = Vector3.zero;  // ������ġ ����.

    public event Action OnRespawn;

    private HealthSystem _healthSystem;
    private PlayerRagdollController _playerRagdollController;

    private void Awake()
    {
        _playerRagdollController = GetComponent<PlayerRagdollController>();
        _healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneCheck(scene);
        _savePoint = _startPoint;
        _healthSystem.OnDied += Revive;
        //SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    public void Revive() 
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

        if (Physics.Raycast(transform.position, Vector3.down, out _hit))
        {
            if (_hit.transform.gameObject.CompareTag("SaveBoard"))
            {
                _savePoint = gameObject.transform.position + Vector3.up * 2;
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
            shark.SetShark();
            //Destroy(shark.gameObject);
        }
        gameObject.transform.position = _savePoint;
        _savePoint = _startPoint;
        OnRespawn?.Invoke();
    }

    private void sceneCheck(Scene scene) 
    {
        if (scene.name == "StageScene")
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
