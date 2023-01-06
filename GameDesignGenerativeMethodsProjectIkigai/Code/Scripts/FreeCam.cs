using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    public float sensiX;
    public float sensiY;
    public Transform playerBody;
    float rotationX;
    float rotationY;

    private void Start()
    {
        //will lock the cursor position so it wont leave the screen and stay in the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        //get mouse input via inut manager from unity
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensiX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensiY;

        rotationX -= mouseY;
        //making sure that maximum rotation is 90 degree either upward or downward
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);   
        rotationY += mouseX;
        //rotate cam
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        playerBody.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
