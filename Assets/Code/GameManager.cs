using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu; //must link to settings, restart level, and quit
    public GameObject soundOptionsMenu; // must have slider for music and sound fx volume & text for their int value
    public GameObject nextLevelScreen; // must restart level 
    private GolfBallMove golfBall;
    private MusicClass musicClass;
    [SerializeField] private Material levelSkyBox;

    void Start()
    {
        if(pauseMenu != null) {
            pauseMenu.SetActive(false);
        }
        if (soundOptionsMenu != null) {
            soundOptionsMenu.SetActive(false);
        }
        if(nextLevelScreen != null) {
            nextLevelScreen.SetActive(false);
        } 
        Time.timeScale = 1f;
        GameObject golfBallMaybe = GameObject.FindGameObjectWithTag("GolfBall");
        if (golfBallMaybe != null) {
            golfBall = golfBallMaybe.GetComponent<GolfBallMove>();
        } else {
            print("no golf ball found in game manager");
        }
        GameObject musicMaybe = GameObject.FindGameObjectWithTag("Music");
        if (musicMaybe != null) {
            musicClass = musicMaybe.GetComponent<MusicClass>();
        } else {
            print("no music class found in game manager");
        }
        if (levelSkyBox != null) {
            RenderSettings.skybox = levelSkyBox;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && nextLevelScreen != null) {
            nextLevelScreen.SetActive(true);
        }
        RenderSettings.skybox.SetFloat("_Rotation", Time.time);

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    
    public IEnumerator LoadOptions()
    {
        yield return new WaitForSeconds(0f);
    }
    public void ShowPauseMenu()
    {
        golfBall.paused = true;
        pauseMenu.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
    }
    public void ClosePauseMenu()
    {
        // settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        golfBall.paused = false;
    }

    public void ShowSoundOptionsMenu()
    {
        soundOptionsMenu.SetActive(true);
        golfBall.paused = true;
    }

    public void CloseSoundOptionsMenu()
    {
        soundOptionsMenu.SetActive(false);
        golfBall.paused = false;
    }

    public void ShowNextLevelScreen() 
    {
        nextLevelScreen.SetActive(true);
    }

    public void ChangeSound(int num)
    {
        musicClass.ChangeMusic(num);
    }

    public void controlVolume(float volume){
        musicClass.setVolume(volume);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
    public void LoadNextLevel() 
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 16) {
            SceneManager.LoadScene("Ending");
        } else {
            SceneManager.LoadScene(++currentLevel);
        }
    }

}
