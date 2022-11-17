using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    public bool paused = false;
    private float scaleDownTouch = 1.0f;
    private Rigidbody _rigidbody;
    private LineRenderer lineRenderer;
    private GameManager gameManager;
    private Vector3 force = Vector3.zero;
    private bool ballStoppedMoving = true;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

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
        if (Input.touchCount > 0 && ballStoppedMoving) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                force = new Vector3(-touch.deltaPosition.x * scaleDownTouch, 0, -touch.deltaPosition.y * scaleDownTouch);
                DrawLine(touch);
            } else if (touch.phase == TouchPhase.Ended) {
                _rigidbody.AddForce(force);
            }
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -30.0f) {
            gameManager.RestartLevel();
        }
        
        if (_rigidbody.velocity.magnitude < 0.15f) {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            ballStoppedMoving = true;
        } else {
            lineRenderer.enabled = false;
        }
    }
    private void DrawLine(Touch touch) {
        float initialDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        Vector3 rayPoint = ray.GetPoint(initialDistance);
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(rayPoint.x, transform.position.y, rayPoint.z));
    }

}
