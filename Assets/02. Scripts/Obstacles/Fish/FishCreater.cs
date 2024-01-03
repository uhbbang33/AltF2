using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FishCreater : MonoBehaviour
{
    [SerializeField] private GameObject _fishPrefab;
    [SerializeField] private float _createFishTime = 1f;

    private IObjectPool<FishObstacle> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<FishObstacle>(CreateFish, OnGetFish, OnReleaseFish, OnDestroyFish, maxSize: 40);
    }

    private void Start()
    {
        StartCoroutine(GetFish());
    }

    IEnumerator GetFish()
    {
        while (true)
        {
            FishObstacle fish = _pool.Get();
            //fish.transform.position = Vector3.up;
            yield return new WaitForSeconds(_createFishTime);
        }
    }

    private FishObstacle CreateFish()
    {
        FishObstacle fish = Instantiate(_fishPrefab, transform).GetComponent<FishObstacle>();
        fish.SetManagedPool(_pool);
        
        return fish;
    }

    private void OnGetFish(FishObstacle fish)
    {
        fish.gameObject.SetActive(true);
    }

    private void OnReleaseFish(FishObstacle fish)
    {
        fish.gameObject.SetActive(false);
    }

    private void OnDestroyFish(FishObstacle fish)
    {
        Destroy(fish.gameObject);
    }
}