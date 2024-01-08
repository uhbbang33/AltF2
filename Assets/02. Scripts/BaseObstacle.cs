using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{

    private float _hitCount = 0;
    private string _soundName = "";

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // player damage
            var player = collision.gameObject;
            player.GetComponent<HealthSystem>()?.Hit();
            

            // player lagdoll
            collision.gameObject.GetComponent<PlayerRagdollController>()?.SetRagdollState(true);

            nameset();

            GameManager.Instance.AudioManager.SFXPlay((_soundName), gameObject.transform.position, 0.1f);
        }
    }

    private void nameset() 
    {
        _soundName = gameObject.name.Replace("(Clone)", "");
        for (int i = 0; i < 20; i++)
        {
            _soundName = _soundName.Replace(" (" + i + ")", "");
        }
    }
}
