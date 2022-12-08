using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float speed = 1.5f;
    private float yStart;
    private Vector3 _position;
    private GameManager gameManager;
    private GameObject golfBall;

    public GameObject explosionEffect;

    private void Awake() {
        yStart = transform.position.y;
        _position = transform.position;
        
        GameObject gameManagerMaybe = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerMaybe != null) {
            gameManager = gameManagerMaybe.GetComponent<GameManager>();
            print("FOUND GAMEMANAGER");
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
            Handheld.Vibrate();
            StartCoroutine(SpikeDeath(other.gameObject.transform.position));
        }
    }

    IEnumerator SpikeDeath(Vector3 golfBall_pos) {
        golfBall = GameObject.FindGameObjectWithTag("GolfBall");
        Instantiate(explosionEffect, golfBall_pos, Quaternion.identity);
        Destroy(golfBall);
        yield return new WaitForSeconds(0.5f);
        print("Death by spikes!!!");
        gameManager.RestartLevel();
    }
}
