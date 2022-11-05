using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    public bool paused = false;
    private float scaleDownTouch = 0.3f;
    private Rigidbody _rigidbody;
    private GameManager gameManager;


    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();

        GameObject gameManagerMaybe = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerMaybe != null) {
            gameManager = gameManagerMaybe.GetComponent<GameManager>();
        } else {
            print("No gamemanager found");
        }
    } 

    void Update()
    {
        if(!paused) {
            Time.timeScale = 1;
        } 
        else {
            Time.timeScale = 0;
        }
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                Vector3 force = new Vector3(-touch.deltaPosition.x * scaleDownTouch, 0, -touch.deltaPosition.y * scaleDownTouch);
                _rigidbody.AddForce(force);
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -50.0f) {
            gameManager.RestartLevel();
        }
    }

}
