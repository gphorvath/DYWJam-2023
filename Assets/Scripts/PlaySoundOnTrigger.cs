using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip soundEffect;      // Assign the sound clip in the inspector
    public AudioSource audioSource; // AudioSource component to play the sound

    private void PlaySoundEffect()
    {
        if (audioSource && soundEffect)
        {
            audioSource.clip = soundEffect;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlaySoundEffect();
        }
    }
}
