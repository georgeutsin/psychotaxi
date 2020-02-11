using UnityEngine;

public class StateManager : MonoBehaviour
{

    static string highscoreString = "highscore";
    static string totalCoinsString = "totalCoins";

    private static StateManager stateManager;

    public static StateManager instance
    {
        get
        {
            if (!stateManager)
            {
                stateManager = FindObjectOfType(typeof(StateManager)) as StateManager;

                if (!stateManager)
                {
                    Debug.LogError("There needs to be one active StateManager script on a GameObject in your scene.");
                }
                else
                {
                    stateManager.Init();
                }
            }

            return stateManager;
        }
    }

    void Init()
    {
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(highscoreString, 0);
    }

    public static int SetHighScore(int score)
    {
        if (score > GetHighScore()){
            PlayerPrefs.SetInt(highscoreString, score);
            PlayerPrefs.Save();
        }

        return GetHighScore();
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(totalCoinsString, 0);
    }

    public static int AddCoins(int coins)
    {
        PlayerPrefs.SetInt(totalCoinsString, GetCoins() + coins);
        PlayerPrefs.Save();
        return GetCoins();
    }

    public static int RemoveCoins(int coins)
    {
        PlayerPrefs.SetInt(totalCoinsString, GetCoins() - coins);
        PlayerPrefs.Save();
        return GetCoins();
    }

    public static int SetUpgradeLevel(string upgrade, int level)
    {
        PlayerPrefs.SetInt(upgrade, level);
        PlayerPrefs.Save();

        return level;
    }

    public static int GetUpgradeLevel(string upgrade)
    {
        return PlayerPrefs.GetInt(upgrade, 1);
    }

    public static void ResetAllStats()
    {
        PlayerPrefs.DeleteAll();
    }
}