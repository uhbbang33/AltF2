using System.Collections;
using UnityEngine;

public class BladeController : BaseObstacle
{

    [Header("Routine Speed")]
    public float DropSpeed;
    public float ReloadSpeed;

    [Header("Guillotine Main Object")]
    public GameObject MainObject;

    private Vector3 _maxHeightPos = new Vector3(0, 8, 0);
    private Vector3 _minHeightPos = new Vector3(0, 1, 0);

    private bool _dropBlade;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource=GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (!_dropBlade)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //데미지 입히는 메서드 들어갈 예정
        }
    }

    public void RoutineTrap()
    {
        StartCoroutine(Drop());
    }

    #region RoutineTrap 코루틴
    IEnumerator Drop()
    {
        _dropBlade = true;

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
        _dropBlade = false;

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
