using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject ballGoesUnderHere;
    public bool oscillate = true;
    public float speed = 1.0f;
    public float journeyLength = 1.0f;
    private float startTime;
    void FixedUpdate()
    {
        if (oscillate) {
            float distanceCovered = Mathf.PingPong(Time.time - startTime, journeyLength / speed);
            transform.position = Vector3.Lerp(pointA.position, pointB.position, distanceCovered / journeyLength);
        }
        if (!oscillate) {
            float distanceCovered = (Time.time - startTime) * speed; 
            transform.position = Vector3.Lerp(pointA.position, pointB.position, distanceCovered / journeyLength);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("GolfBall")) {
            other.gameObject.transform.parent = ballGoesUnderHere.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("GolfBall")) {
            other.gameObject.transform.parent = transform.parent.parent;
        }
    }
}
