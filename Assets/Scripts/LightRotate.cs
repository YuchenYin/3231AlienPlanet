using UnityEngine;

public class LightRotate : MonoBehaviour
{
    public float rotateSpeed = 10f; // rotate speed
    private float totalRotation = 0f; // total rotation angles
    private bool rotateBack = false; // should rotate back
    public float rotateAngle = 30f; // rotate angle

    void Update()
    {
        if (totalRotation < rotateAngle && !rotateBack)
        {
            // calculate rotate angles should applying this frame
            float rotationAmount = rotateSpeed * Time.deltaTime;
            // rotate obj
            transform.Rotate(Vector3.up, rotationAmount);
            // update total rotation
            totalRotation += Mathf.Abs(rotationAmount);
            // if rotation angle larger than aimed, rotate back
            if (totalRotation >= rotateAngle)
            {
                rotateBack = true;
            }
        }
        else if (rotateBack)
        {
            // calculate rotate angles should applying this frame
            float rotationAmount = rotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, -rotationAmount);
            totalRotation -= Mathf.Abs(rotationAmount);
            // if back to origin place ,stop rotate
            if (totalRotation <= 0f)
            {
                rotateBack = false;
            }
        }
    }
}
