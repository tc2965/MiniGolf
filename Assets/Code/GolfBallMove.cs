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
    private LineRenderer lineRenderer;
    private List<Vector3> linePoints = new List<Vector3>();

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null) {
            print("no line found");
        }
        linePoints.Add(transform.position);
        linePoints.Add(transform.position);
        lineRenderer.SetPositions(linePoints.ToArray());

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
                    linePoints[0] = transform.position;
                    ballTurn = true;
                }
            } else if (touch.phase == TouchPhase.Moved && ballTurn) {
                lineRenderer.gameObject.SetActive(ballTurn);
                ballMoveForce = new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y);
                linePoints[1] = transform.position - ballMoveForce;
                lineRenderer.SetPositions(linePoints.ToArray());
            } else if (touch.phase == TouchPhase.Ended && ballTurn) {
                ballTurn = false;
                _rigidbody.AddForce(ballMoveForce);
                lineRenderer.gameObject.SetActive(ballTurn);
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