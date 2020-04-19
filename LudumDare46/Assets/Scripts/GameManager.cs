using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    private void PauseGame()
    {
        smenu.gameObject.SetActive(true);
        currentState = GameState.Pause;
        audioManager.audiosource.Pause();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("resume");
        smenu.gameObject.SetActive(false);
        currentState = GameState.Playing;
        audioManager.audiosource.UnPause();
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        smenu.MainMenu();
        ResumeGame();
    }

    public void ActiveSceneChange(Scene current, Scene next)
    {
        audioManager.SwitchAudio(next.buildIndex);
        if (next.buildIndex == 1)
            currentState = GameState.Playing;
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
