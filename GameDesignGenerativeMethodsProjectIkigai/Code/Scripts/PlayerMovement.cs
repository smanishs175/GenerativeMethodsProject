using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController; 
    public float speed=12f;
    //Vector3 velocity;
    //public float gravity = -9.81f;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.Q))
        {
            move += transform.up;
        }
        if(Input.GetKey(KeyCode.E))
        {
            move -= transform.up;
        }
        // move.y = move.y + Physics.gravity.y * Time.deltaTime;
        characterController.Move(move * speed * Time.deltaTime);
        //characterController.Move(velocity * Time.deltaTime);
    }
}
