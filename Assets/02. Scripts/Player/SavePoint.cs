using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _startPoint; // 시작위치 설정.
    private Vector3 _firstStartPoint = new Vector3(10,10,10); // 1스테이지 시작위치 설정.
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
    }

    /*private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        sceneCheck(scene);
        _savePoint = _startPoint;
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SaveBoard")
        {
            _savePoint = collision.transform.position + Vector3.up;
            //세이브 이펙트
            Destroy(collision.gameObject,1f);
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
        if (scene.name == "KJW")
        {
            _startPoint = _firstStartPoint;
        }
        else if (scene.name == "99.BJH")
        {
            _startPoint = _SecondStartPoint;
        }
    }
}
