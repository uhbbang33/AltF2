using UnityEngine;

public class GuillotineEventHandler : MonoBehaviour
{
    public float Delay;
    public GameObject guillotineBlade;

    [HideInInspector]
    public bool _triggerInPlayer { get; private set; }

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
