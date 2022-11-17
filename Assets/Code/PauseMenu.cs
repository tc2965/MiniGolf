using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu; 
    public GameObject golf;
    
    void Start(){
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        golf = GameObject.FindGameObjectWithTag("GolfBall");
        GolfBallMove script = golf.GetComponent<GolfBallMove>();
        script.paused = true; 
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        golf = GameObject.FindGameObjectWithTag("GolfBall");
        GolfBallMove script = golf.GetComponent<GolfBallMove>();
        script.paused = false;
    }

    public void Home(int sceneID){
        Time.timeScale = 1f; 
        SceneManager.LoadScene(sceneID);
    }
}
