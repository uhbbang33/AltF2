using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleEffectManager : MonoBehaviour
{
    public ParticleSystem waterParticle;
    public static ParticleEffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySeaParticle(Vector3 position)
    {
        ParticleSystem particle = Instantiate(waterParticle, position, Quaternion.identity);

        particle.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        particle.Play();
    }

}
