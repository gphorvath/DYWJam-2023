using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public string nextLevelName = "Start";
    public AudioSource audioSource;
    public AudioClip soundEffect;

    private bool levelEnded = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelEnded)
        {
            levelEnded = true;

            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer)
            {
                timer.StopLevel();
            }

            PlaySoundEffect();

            Debug.Log($"Setup end level buttons for {nextLevelName}");

            Leaderboard.Instance.SetupEndLevelButtons(nextLevelName);
            Leaderboard.Instance.ShowLeaderboard();

            // Deactivate this game object after processing everything
            gameObject.SetActive(false);
        }
    }

    private void PlaySoundEffect()
    {
        if (audioSource && soundEffect)
        {
            Debug.Log("Playing sound effect");
            audioSource.clip = soundEffect;
            audioSource.Play();
        }
    }
}
