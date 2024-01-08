using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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

    public void Playfeather(GameObject chicken)
    {
        StartCoroutine(feather(chicken));
    }

    IEnumerator feather(GameObject chicken)
    {
        int count = 0;
        while (count<=3&& chicken!=null) 
        { 
            ParticleSystem FeaterEffect = Resources.Load<ParticleSystem>("FeaterEffect");
            ParticleSystem particle = Instantiate(FeaterEffect, chicken.transform.position, Quaternion.identity);
            particle.Play();
            yield return new WaitForSeconds(0.3f);
            count++;
        }
    }

    //public void PlayFirstBloodParticle()
    //{
    //    ParticleSystem[] particels = firsHitBloodParticle.GetComponentsInChildren<ParticleSystem>();

    //    foreach(ParticleSystem particle in particels)
    //    {
    //        particle.Play();
    //    }
    //}

    //public void PlaySecondBloodParticle()
    //{
    //    ParticleSystem[] particels = secondHitBloodParticle.GetComponentsInChildren<ParticleSystem>();

    //    foreach (ParticleSystem particle in particels)
    //    {
    //        particle.Play();
    //    }
    //}

    public void PlayBloodParticle(GameObject bloodParticle)
    {
        ParticleSystem[] particles = bloodParticle.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }

    // 파티클 리셋
    public void ResetParticle(GameObject particleObject)
    {
        if (particleObject != null)
        {
            ParticleSystem[] particles = particleObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particle in particles)
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); 
            }
        }
    }

}
