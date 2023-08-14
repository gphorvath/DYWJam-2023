using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;

    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    public int maxEntries = 10;
    public TextMeshProUGUI leaderboardText;
    public GameObject leaderboardContainer; // This contains the leaderboard and the buttons
    public UnityEngine.UI.Button nextLevelButton; // Reference to the Next Level button in the UI
    public UnityEngine.UI.Button replayButton; // Reference to the Replay button in the UI

    private string _filePath = "";

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string levelName;
        public float time;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _filePath = Path.Combine(Application.persistentDataPath, "leaderboard.csv");

        LoadLeaderboard();
        HideLeaderboard();
    }

    public void SubmitNewTime(string level, float time)
    {
        leaderboardEntries.Add(new LeaderboardEntry { levelName = level, time = time });

        // Filter and sort entries for the specific level
        var entriesForLevel = leaderboardEntries.Where(e => e.levelName == level).OrderBy(e => e.time).ToList();

        while (entriesForLevel.Count > maxEntries)
        {
            var lastEntry = entriesForLevel.Last();
            leaderboardEntries.Remove(lastEntry); // Remove from main list
            entriesForLevel.Remove(lastEntry);    // Remove from level-specific list
        }

        SaveLeaderboard();
        DisplayLeaderboardForLevel(level);
    }


    public void ShowLeaderboard()
    {
        leaderboardContainer.SetActive(true);
    }

    void DisplayLeaderboardForLevel(string level)
    {
        leaderboardText.text = "";
        var entriesForLevel = leaderboardEntries.Where(e => e.levelName == level).OrderBy(e => e.time).ToList();

        for (int i = 0; i < entriesForLevel.Count; i++)
        {
            int minutes = (int)(entriesForLevel[i].time / 60);
            int seconds = (int)(entriesForLevel[i].time % 60);
            int milliseconds = (int)((entriesForLevel[i].time * 100) % 100);

            leaderboardText.text += $"{i + 1}. {minutes:00}:{seconds:00}.{milliseconds:00}\n";
        }

        ShowLeaderboard();
    }

    void SaveLeaderboard()
    {
        using (StreamWriter writer = new StreamWriter(_filePath, false))
        {
            writer.WriteLine("LevelName,Time");
            foreach (var entry in leaderboardEntries)
            {
                writer.WriteLine($"{entry.levelName},{entry.time}");
            }
        }
    }

    void LoadLeaderboard()
    {
        if (File.Exists(_filePath))
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                // Read header line
                reader.ReadLine();
                Debug.Log($"Loading leaderboard from {_filePath}");

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    leaderboardEntries.Add(new LeaderboardEntry { levelName = values[0], time = float.Parse(values[1]) });
                }
            }
        }
    }

    public void SetupEndLevelButtons(string nextSceneName)
    {
        // Setup the Next Level button functionality with the provided scene name
        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(() => { LoadNextLevel(nextSceneName); });

        // Setup the Replay button functionality
        replayButton.onClick.RemoveAllListeners();
        replayButton.onClick.AddListener(() => { ReplayLevel(); });
    }

    public void HideLeaderboard()
    {
        leaderboardContainer.SetActive(false);
    }

    private void LoadNextLevel(string sceneName)
    {
        Debug.Log($"Loading scene {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    private void ReplayLevel()
    {
        Debug.Log($"Reloading scene {SceneManager.GetActiveScene().name}");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
