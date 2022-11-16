using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    public bool paused = false;
    private float scaleDownTouch = 0.3f;
    private Rigidbody _rigidbody;
    private GameManager gameManager;
    private bool ballTurn = false;

    private Vector3 ballMoveForce;
    

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
            if (touch.phase == TouchPhase.Began) {
                RaycastHit hit = getScreenPointToRay(touch.position);
                if (hit.collider.name == "GolfBall") {
                    ballTurn = true;
                }
            } else if (touch.phase == TouchPhase.Moved && ballTurn) {
                ballMoveForce = new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y);
            } else if (touch.phase == TouchPhase.Ended && ballTurn) {
                ballTurn = false;
                _rigidbody.AddForce(ballMoveForce);
            }
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -30.0f) {
            gameManager.RestartLevel();
        }
    }

    private RaycastHit getScreenPointToRay(Vector2 inputPosition) {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit; 
        Physics.Raycast(ray, out hit);
        return hit;
    }

}