using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required for UI elements

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip[] tracks;
    private int currentTrack = 0;
    private AudioSource audioSource;
    public float fadeDuration = 1f;
    private bool transitioning = false;

    public Slider playbackSlider; // The UI Slider for visualizing playback progress

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (tracks.Length > 0)
        {
            currentTrack = Random.Range(0, tracks.Length);
            audioSource.clip = tracks[currentTrack];
            audioSource.Play();
        }
    }

    void Update()
    {
        // If current track is about to finish playing (time remaining <= fadeDuration)
        if (audioSource.time >= audioSource.clip.length - fadeDuration && !transitioning)
        {
            PlayNextTrack();
            transitioning = true;
        }
    }

    void FixedUpdate()
    {
        // Update the slider value based on playback progress
        if (playbackSlider && audioSource.clip)
        {
            playbackSlider.value = audioSource.time / audioSource.clip.length;
        }
    }

    void PlayNextTrack()
    {
        currentTrack = Random.Range(0, tracks.Length);
        StartCoroutine(FadeOutAndPlayNext());
    }

    IEnumerator FadeOutAndPlayNext()
    {
        // Fade out current track
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        // Move to the next track
        currentTrack = (currentTrack + 1) % tracks.Length;
        audioSource.clip = tracks[currentTrack];
        audioSource.Play();

        // Fade in new track
        audioSource.volume = 0;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }

        transitioning = false;
    }
}
