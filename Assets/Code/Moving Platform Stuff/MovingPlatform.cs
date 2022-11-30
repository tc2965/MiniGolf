using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject GolfBall;

    private void OnTriggerEnter(Collider other) {
        GolfBall.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other) {
        GolfBall.transform.parent = null;
    }
}
