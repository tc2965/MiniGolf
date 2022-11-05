using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    public bool paused = false;
    private float scaleDownTouch = 0.3f;
    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
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

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Spike")) {

    //     }
    // }
    
}
