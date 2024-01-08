using System.Collections;
using UnityEngine;

public class BladeController : BounceObstacle
{

    [Header("Routine Speed")]
    public float DropSpeed;
    public float ReloadSpeed;

    [Header("Guillotine Main Object")]
    public GameObject MainObject;

    private Vector3 _maxHeightPos = new Vector3(0, 8, 0);
    private Vector3 _minHeightPos = new Vector3(0, 1, 0);

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource=GetComponent<AudioSource>();
    }

    public void RoutineTrap()
    {
        Debug.Log("Routine");
        StopAllCoroutines();
        StartCoroutine(Drop());
    }

    #region RoutineTrap ÄÚ·çÆ¾
    IEnumerator Drop()
    {
        while (transform.localPosition != _minHeightPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _minHeightPos, DropSpeed * Time.deltaTime);
            yield return null;
        }
        _audioSource.Play();
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        while (transform.localPosition != _maxHeightPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _maxHeightPos, ReloadSpeed * Time.deltaTime);
            yield return null;
        }
        if (MainObject.GetComponent<GuillotineEventHandler>()._triggerInPlayer)
        {
            StartCoroutine(Drop());
        }
    }
    #endregion
}
