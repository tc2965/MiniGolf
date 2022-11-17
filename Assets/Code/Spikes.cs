using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float speed = 1.5f;
    private float yStart;
    private Vector3 _position;
    private GameManager gameManager;

    private void Awake() {
        yStart = transform.position.y;
        _position = transform.position;
        
        GameObject gameManagerMaybe = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerMaybe != null) {
            gameManager = gameManagerMaybe.GetComponent<GameManager>();
        } else {
            print("No gamemanager found");
        }
    }

    void Update()
    {
        _position.y = yStart + 2.0f * Mathf.Sin(speed * Time.fixedTime);
        transform.position = _position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GolfBall")) {
            // gameManager.RestartLevel();
        }
    }
}
