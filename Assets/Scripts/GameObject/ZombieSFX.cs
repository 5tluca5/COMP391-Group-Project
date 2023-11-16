using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSFX : MonoBehaviour
{

    public AudioClip spawnClip;
    public AudioClip hitClip;
    public AudioClip deadClip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySound(spawnClip);
    }

    public void PlayHitSound() 
    {
        AudioManager.Instance.PlaySound(hitClip);
    }

    public void PlayDeadSound()
    {
        AudioManager.Instance.PlaySound(deadClip);
    }
}
