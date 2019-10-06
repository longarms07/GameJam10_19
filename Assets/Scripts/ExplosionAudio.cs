using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    public AudioClip explosionClip;
    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();

    }

    public void explode()
    {
        audioS.PlayOneShot(explosionClip);
    }

}
