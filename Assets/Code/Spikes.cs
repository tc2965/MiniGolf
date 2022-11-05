using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float speed = 1.5f;
    private float yStart;
    private Vector3 _position;

    void Start()
    {
        yStart = transform.position.y;
        _position = transform.position;
    }

    void Update()
    {
        _position.y = yStart + 2.0f * Mathf.Sin(speed * Time.fixedTime);
        transform.position = _position;
    }
}
