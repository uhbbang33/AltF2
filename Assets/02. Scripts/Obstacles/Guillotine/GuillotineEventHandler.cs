using UnityEngine;

public class GuillotineEventHandler : BaseObstacle
{
    public float Delay;
    public GameObject guillotineBlade;
    private BladeController _controller;

    [HideInInspector]
    public bool _triggerInPlayer { get; private set; }

    private void Awake()
    {
        _controller = guillotineBlade.GetComponent<BladeController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerInPlayer = true;
            _controller.RoutineTrap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerInPlayer = false;
        }
    }
}
