using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _startPoint; // 시작위치 설정.
    private Vector3 _firstStartPoint = new Vector3(-1,52,44); // 1스테이지 시작위치 설정.
    private Vector3 _SecondStartPoint = new Vector3(94, 0, 15); // 2스테이지 시작위치 설정.

    private Vector3 _savePoint = Vector3.zero;  // 저장위치 설정.
    

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneCheck(scene);
        _savePoint = _startPoint;

        //SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(ReStartCo());
        }
        checkSaveBoard();
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.3f);
    }

    /*private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        sceneCheck(scene);
        _savePoint = _startPoint;
    }*/

    private void checkSaveBoard() 
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 1))
        {
            if (_hit.transform.CompareTag("SaveBoard"))
            {
                Debug.Log("세이브보드");
                _savePoint = _hit.transform.position + Vector3.up;
                //세이브 이펙트
                Destroy(_hit.collider.gameObject, 1f);
            }
        }
    }
    
    IEnumerator ReStartCo()
    {
        //피이펙트
        yield return new WaitForSeconds(1f);
        gameObject.transform.position = _savePoint;
        _savePoint = _startPoint;
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
    }
}
