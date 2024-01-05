using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private float _jumpforce = 40f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 forwardDirection = collision.transform.up;
        if (otherRigidbody != null)
        {
            otherRigidbody.AddForce(forwardDirection * _jumpforce, ForceMode.Impulse);
            GameManager.Instance.AudioManager.SFXPlay(("Pivot"), gameObject.transform.position, 0.1f);
        }
        
    }
    
}
