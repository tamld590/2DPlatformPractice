using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Finish")]
    [SerializeField] private GameObject finishWinScreen;
    [SerializeField] private AudioClip finishWinSound;

    [Header ("Paused")]
    [SerializeField] private GameObject pauseScreen;
    private Text textComponent;
    //private PlayerRespawn playerRespawn;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        finishWinScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else 
                PauseGame(true);
        }
    }
    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOverScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion
    #region Pause
    public void PauseGame(bool status)
    {

        pauseScreen.SetActive(status);
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        } 
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
    #region MenuStart
    public void StartMenu()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 3));
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 8));
    }
    public void Setting()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }
    public void Credits()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 2));
    }
    #endregion
    #region FinishWin
    public void FinishWin()
    {
        finishWinScreen.SetActive(true);
        SoundManager.instance.PlaySound(finishWinSound);
    }
    #endregion
    #region Levels
    public void Level1()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 3));
    }
    public void Level2()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 4));
    }
    public void Level3()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 5));
    }
    public void Level4()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 6));
    }
    public void Level5()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 7));
    }
    #endregion
}
