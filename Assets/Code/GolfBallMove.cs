using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMove : MonoBehaviour
{
    private float scaleDownTouch = 0.1f;
    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    } 

    void FixedUpdate()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) {
                _rigidbody.AddForce(new Vector3(-touch.deltaPosition.x * scaleDownTouch, 0, -touch.deltaPosition.y * scaleDownTouch));
            }
        }
    }
    
}
