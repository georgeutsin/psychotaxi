using UnityEngine;

public class StateManager : MonoBehaviour
{

    private static string highscoreString = "highscore";
    private static string totalCoinsString = "totalCoins";

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
        return PlayerPrefs.GetInt(highscoreString);
    }

    public static void SetHighScore(int score)
    {
        if (score > GetHighScore()){
            PlayerPrefs.SetInt(highscoreString, score);
            PlayerPrefs.Save();
        }
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(totalCoinsString);
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
        if (PlayerPrefs.HasKey(upgrade))
        {
            return PlayerPrefs.GetInt(upgrade);
        }
        else
        {
            SetUpgradeLevel(upgrade, 1);
            return 1;
        }
    }

    public static void ResetAllStats()
    {
        PlayerPrefs.DeleteAll();
    }
}