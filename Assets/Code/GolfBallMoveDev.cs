using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMoveDev : MonoBehaviour
{
   public bool paused = false;
    private float scaleDownTouch = 0.3f;
    private Rigidbody _rigidbody;
    private GameManager gameManager;
    private bool ballTurn = false;
    private Vector3 ballMoveForce = Vector3.zero;
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

        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Q)) {
            print("checking...");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider.name == "GolfBall") {
                print("hit ball");
                linePoints[0] = transform.position;
                ballTurn = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.W)) {
            print("releasing ball");
            ballTurn = false;
            _rigidbody.AddForce(ballMoveForce);
            ResetLineRenderer();
        }
        if (ballTurn) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            ballMoveForce = scaleDownTouch * new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
            linePoints[1] = transform.position - ballMoveForce;
            lineRenderer.SetPositions(linePoints.ToArray());
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

    private void ResetLineRenderer() {
        linePoints[0] = transform.position;
        linePoints[1] = transform.position;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
