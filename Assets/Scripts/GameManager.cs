using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Play,
    Lose,
    Win
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; protected set; }
    public static GameState gameState = GameState.Start;
    public int level;
    public List<Level> levels = new List<Level>();
    [HideInInspector] public Level selectedLevel;

    void Awake()
    {
        Instance = this;
        level = PlayerPrefs.GetInt("level", 1);
        UIManager.Instance.SetPanel();
        //TinySauce.OnGameStarted(level.ToString());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameState == GameState.Start)
        {
            UIManager.Instance.SetPanel(GameState.Play);
        }
    }

    public void Win()
    {
        UIManager.Instance.SetPanel(GameState.Win);
        //TinySauce.OnGameFinished(true, 1, level.ToString());
    }


    public void Lose()
    {
        UIManager.Instance.SetPanel(GameState.Lose);
        //TinySauce.OnGameFinished(false, 1, level.ToString());
    }

    [SkipRename]
    public void Restart()
    {
        AudioManager.Instance.Vibrate();
        SceneManager.LoadScene(0);
    }

    [SkipRename]
    public void Next()
    {
        AudioManager.Instance.Vibrate();
        level++;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(0);
        LoadLevel();
    }

    private void LoadLevel()
    {
        var l = level;
        if (l > levels.Count)
            l = Random.Range(2, levels.Count + 1);

        selectedLevel = levels[l - 1];
        selectedLevel.gameObject.SetActive(true);
    }
}