using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip GetJewelSound;
    public AudioClip DeadSound;
    public AudioClip HitSound;
    public AudioClip IsHittedSound;
    public static AudioController Instance;
    AudioSource audioSource;
    void Start()
    {
        if(AudioController.Instance == null)
        {
            AudioController.Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = DataVariables.SoundVolume;
        
    }
    public void HitSoundPlay()
    {
        audioSource.PlayOneShot(HitSound);
    }
    public void GetJewelSoundPlay()
    {
        audioSource.PlayOneShot(GetJewelSound);
    }
    public void DeadSoundPlay()
    {
        audioSource.PlayOneShot(DeadSound);
    }
    public void HittedSoundPlay()
    {
        audioSource.PlayOneShot(IsHittedSound);
    }
    private void Update() {
        //later put off
        audioSource.volume = DataVariables.SoundVolume;
    }
}
