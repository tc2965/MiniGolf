using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        GameObject gameManagerMaybe = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerMaybe != null) {
            gameManager = gameManagerMaybe.GetComponent<GameManager>();
        } else {
            print("No gamemanager found");
        }
    }

    private void OnTriggerEnter(Collider other) {
        print("hit something");
        if (other.CompareTag("GolfBall")) {
            gameManager.ShowNextLevelScreen();
            GolfBallMove? script = other.GetComponent<GolfBallMove>();
            if (script == null) {
                print("not the golf ball, object is tagged wrong");
            } else {
                script.paused = true;
            }
        }
    }
}
