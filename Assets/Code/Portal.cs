using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject otherPortal;
    private float speed = 0.5f;
    private float yStart;
    private Vector3 _position;

    void Start()
    {
        yStart = transform.position.y;
        _position = transform.position;
    }
    void Update()
    {
        _position.y = yStart + 0.1f * Mathf.Sin(speed * Time.fixedTime);
        transform.position = _position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("GolfBall")) {
            other.transform.position = otherPortal.transform.position;
        }
    }
}
