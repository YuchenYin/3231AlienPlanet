using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float gravity = -9.8f; // gravity applied
    public float hightLimit = 10f; // jump height limit
    public Transform checkGround; // ground Calibration, obsolete
    public float groundDist = 0.65f;
    bool isOnGround;
    public LayerMask groundMask;
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isOnGround = Physics.CheckSphere(checkGround.position, groundDist, groundMask);
        if (isOnGround && velocity.y < 0) {
            velocity.y = -2f;        
        }
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") ) {
            velocity.y = Mathf.Sqrt(hightLimit * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}
