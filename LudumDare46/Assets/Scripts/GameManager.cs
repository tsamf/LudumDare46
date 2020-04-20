using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    None = 0,
    Playing,
    Pause,
    Menu,
}

public class GameManager : MonoBehaviour
{
    [Header("set in inspector")]
    public SettingsMenu smenu = null;
    public GameObject gameoverMenu = null;
    public GameObject HUD = null;
    public TextMeshProUGUI scoreTxt = null;
    public TextMeshProUGUI gameoverScoreTxt = null;

    public ScoreManager scoreManager = null;
    public AudioManager audioManager = null;

    public static GameManager instance = null;
    public GameState currentState = GameState.None;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.activeSceneChanged += ActiveSceneChange;
        }
    }

    private void Start()
    {
        scoreManager = ScoreManager.instance;
        audioManager = AudioManager.instance;
        scoreManager.onScoreChange += UpdateScoreUI;
    }

    public void UpdateScoreUI(int score)
    {
        scoreTxt.text = score.ToString();
    }

    private void PauseGame()
    {
        HUD.gameObject.SetActive(false);
        smenu.gameObject.SetActive(true);
        currentState = GameState.Pause;
        audioManager.audiosource.Pause();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        scoreManager.ResetScore();
        HUD.gameObject.SetActive(true);
        gameoverMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        HUD.gameObject.SetActive(true);
        smenu.gameObject.SetActive(false);
        gameoverMenu.SetActive(false);
        currentState = GameState.Playing;
        audioManager.audiosource.UnPause();
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        smenu.MainMenu();
        smenu.gameObject.SetActive(false);
        currentState = GameState.Menu;
        audioManager.audiosource.UnPause();
        Time.timeScale = 1f;
        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverScoreTxt.text = scoreManager.currentScore.ToString();
        HUD.gameObject.SetActive(false);
        gameoverMenu.SetActive(true);
    }

    public void ActiveSceneChange(Scene current, Scene next)
    {
        audioManager.SwitchAudio(next.buildIndex);
        if (next.buildIndex == 1)
        { 
            currentState = GameState.Playing;
            HUD.gameObject.SetActive(true);
        }
    }

    private void Update()
    {     
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {          
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}
