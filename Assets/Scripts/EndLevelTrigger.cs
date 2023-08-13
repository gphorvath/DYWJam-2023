using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    public string nextLevelName = "Start"; // Specify the name of the next level here

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

            Debug.Log($"Setup end level buttons for {nextLevelName}");

            Leaderboard.Instance.SetupEndLevelButtons(nextLevelName);
            Leaderboard.Instance.ShowLeaderboard();

            // Deactivate this game object after processing everything
            gameObject.SetActive(false);
        }
    }
}
