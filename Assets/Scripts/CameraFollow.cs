using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // target to track
    public float mouseSpeed = 200f;
    private float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limit sight for look up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        target.Rotate(Vector3.up * mouseX);
    }
}
