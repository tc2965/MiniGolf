using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public GameObject currentCinemachineCamera;
    private Vector3 currentOrientation = new Vector3(15.748f, 0.0f, 0.0f);
    private void FixedUpdate() {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("GolfBall")) {
            // currentOrientation = currentCinemachineCamera.transform.eulerAngles;
            currentCinemachineCamera.SetActive(false);
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("GolfBall")) {
            currentCinemachineCamera.SetActive(true);
            // currentCinemachineCamera.transform.eulerAngles = currentOrientation;
            other.gameObject.transform.parent = transform.parent.parent;
            other.gameObject.transform.eulerAngles = new Vector3(
                other.gameObject.transform.eulerAngles.x, 
                0, 
                other.gameObject.transform.eulerAngles.z
            );
        }
    }
}
