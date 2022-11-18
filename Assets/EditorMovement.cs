using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float moveSpeed = 10;
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        //Creates a movement vector local to the direction the player is facing.
        Vector3 moveDir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxis("Horizontal"); // Use GetAxisRaw for snappier but non-analogue  movement
        moveDir *= moveSpeed;
        moveDir.y = _rigidbody.velocity.y; // We dont want y so we replace y with that the _rigidbody.velocity.y already is.
        _rigidbody.velocity = moveDir; // Set the velocity to our movement vector
    }
}
