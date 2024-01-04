using UnityEngine;

public class GuillotineEventHandler : BaseObstacle
{
    public float Delay;
    public GameObject guillotineBlade;

    [HideInInspector]
    public bool _triggerInPlayer { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerInPlayer = true;
            guillotineBlade.GetComponent<BladeController>().RoutineTrap();
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
