using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    // public Rigidbody rb;
    // public float MoveSpeed= 1f;
    // public float sidewaysForce = 5f;
 
    public float moveSpeed = 15;
    private Vector3 moveDir;

    void Update()
    {

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;

        // // move forward
        // Vector3 fw = transform.forward * Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        // // move lateral
        // Vector3 lateral = transform.right * Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        
        // // put it together
        // transform.Translate( fw + lateral);
    }

    void FixedUpdate(){
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}