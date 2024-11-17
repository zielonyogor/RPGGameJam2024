using System.Collections.Generic;
using UnityEngine;

public class VacuumSounds : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip pop1, pop2, pop3, pop4;
    public AudioClip start, loop, end, singleSucc;
    private AudioClip[] allPopSounds;
    
    void Start()
    {
        allPopSounds = new AudioClip[] {pop1, pop2, pop3, pop4};
    }

    public void PlayPopSound()
    {
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.PlayOneShot(allPopSounds[Random.Range(0, allPopSounds.Length)]);
    }

    public void PlaySingleSuccSound()
    {
        audioSource.PlayOneShot(singleSucc);
    }
}
