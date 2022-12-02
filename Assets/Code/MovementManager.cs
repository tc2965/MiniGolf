using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{   
    public bool isFlat = true;
    private Rigidbody rigid;
    public float jumpHeight = 550;
    public bool isGrounded = false;


    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 tilt = Input.acceleration;

        if (isFlat){
            tilt = Quaternion.Euler(135,0,0) * tilt * 8;
        }

        rigid.AddForce(tilt);
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);

    }

    public void Jump()
    {
        if(isGrounded)
        {
            rigid.AddForce(new Vector3(0, jumpHeight, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}

