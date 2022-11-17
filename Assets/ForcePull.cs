using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : MonoBehaviour
{
    public Vector3 currentPosition;
    private bool ballTurnStarted = false;
    private float followSpeed = 0.1f;
    private Vector3 velocity = Vector3.zero; 
    private void Start() {
        currentPosition = getGolfBallPosition();
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                if (TouchedHitBall(touch.position)) {
                    StartCoroutine(DragUpdate(touch));
                }
            }
        }
    }

    private IEnumerator DragUpdate(Touch touch) {
        float initialDistance = Vector3.Distance(currentPosition, Camera.main.transform.position);
        while (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended) {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            transform.position = Vector3.SmoothDamp(transform.position, ray.GetPoint(initialDistance), ref velocity, followSpeed);
            yield return null;
        }
    }

    private Vector3 getGolfBallPosition() {
        return this.transform.parent.position;
    }

    private bool TouchedHitBall(Vector2 touchPosition) {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.name == "GolfBall" || hit.collider.gameObject == this) {
                print("touched golf ball");
                ballTurnStarted = true;
                return true;
            }
        }
        return false;
    }
}
