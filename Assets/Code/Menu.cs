using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    void Start(){
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    public void GoDemo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
