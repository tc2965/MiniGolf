using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    public bool paused = false;
    private float scaleDownTouch = 0.3f;
    private Rigidbody _rigidbody;
    private GameManager gameManager;
    private bool ballTurn = true;
    private bool ballIdle = true;
    private Vector3? lastProcessedWorldPoint;
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
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -30.0f) {
            gameManager.RestartLevel();
        }

        if (_rigidbody.velocity.magnitude < 0.5f) {
            StopBall();
        }

        ProcessAim();
    }

    private void ProcessAim() {
        // if the ball is moving, then we don't wanna interrupt
        // if the ball's turn is still going
        // if (!ballIdle || !ballTurn) {
        //     print("ball idle: " + ballIdle.ToString()); 
        //     print("ball turn: " + ballTurn.ToString()); 
        //     print("ball movement: " + _rigidbody.velocity.magnitude.ToString());
        //     return;
        // }
        
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                // just wanna know if the user intentionally touched the ball
                if (!TouchedHitBall(touch.position)) {
                    return;
                }
            } else if (touch.phase == TouchPhase.Moved) {
                // if the user moved, we wanna know where their finger is
                lastProcessedWorldPoint = ProcessTouchInput(touch.position);

                if (!lastProcessedWorldPoint.HasValue) {
                    return;
                }
                // and if we can draw the line and shoot
                DrawLine(lastProcessedWorldPoint.Value);

            } else if (touch.phase == TouchPhase.Ended) {
                ballTurn = false; 
                lineRenderer.enabled = false;
                Shoot(lastProcessedWorldPoint.Value);
            }
        } else {
            if (lineRenderer.enabled && ballTurn) {
                ballTurn = false; 
                lineRenderer.enabled = false;
            }
        }
    }

    private void Shoot(Vector3 worldPoint) {
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y + 0.0001f, worldPoint.z);
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);
        _rigidbody.AddForce(-direction * strength * 50);
        ballIdle = false;
    }

    private void DrawLine(Vector3 worldPoint) {
        linePoints[0] = transform.position;
        linePoints[1] = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        lineRenderer.SetPositions(linePoints.ToArray());
        lineRenderer.enabled = true;
    }

    private void StopBall() {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        ballIdle = true;
        ballTurn = true;
    }

    private Vector3? ProcessTouchInput(Vector2 touchPosition) {
        float initialDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        return ray.GetPoint(initialDistance);
    }

    private bool TouchedHitBall(Vector2 touchPosition) {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.name == "GolfBall") {
                ballTurn = true;
                return true;
            }
        }

        return false;
    }
}