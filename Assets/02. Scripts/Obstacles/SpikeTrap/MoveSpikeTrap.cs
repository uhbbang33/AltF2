using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikeTrap : MonoBehaviour
{

    private bool _trapTime = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player"&& _trapTime)
        {
            StartCoroutine(MoveSpike());
        }
    }

    IEnumerator MoveSpike()
    {
        _trapTime = false;
        yield return new WaitForSeconds(1f);
        GMTest.Instance.audioManager.SFXPlay(("Spikes"), gameObject.transform.position, 0.1f);
        while (gameObject.transform.localPosition.y < 0.5f) 
        {
            gameObject.transform.position += Vector3.up * 0.01f;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (gameObject.transform.localPosition.y > 0f)
        {
            gameObject.transform.position += Vector3.down * 0.01f;
            yield return null;
        }
        _trapTime = true;
    }
}
