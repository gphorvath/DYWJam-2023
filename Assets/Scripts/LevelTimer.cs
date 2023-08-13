using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public bool isLevelActive = false;
    private float elapsedTime = 0.0f;

    public TextMeshProUGUI timerText;

    private void Update()
    {
        if (isLevelActive)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartLevel()
    {
        elapsedTime = 0.0f;
        isLevelActive = true;
    }

    public void StopLevel()
    {
        isLevelActive = false;
        Debug.Log($"{SceneManager.GetActiveScene().name} ended after {elapsedTime} seconds");
        Leaderboard.Instance.SubmitNewTime(SceneManager.GetActiveScene().name, elapsedTime);
    }

    void UpdateTimerDisplay()
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime * 100) % 100);

        timerText.text = $"{minutes:00}:{seconds:00}.{milliseconds:00}";
    }
}
