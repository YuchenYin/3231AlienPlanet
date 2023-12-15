using UnityEngine;

public class HorizontalRotation : MonoBehaviour
{
    public float radius = 5f; // Rotate radius
    public float speed = 5f; // Rotate speed

    private Vector3 centerPosition; // parents object position
    private float angle; // current angele

    void Start()
    {
        if (transform.parent != null)
        {
            centerPosition = transform.parent.position; // get parent position
        }
        else
        {
            Debug.LogError("HorizontalRotation script requires a parent object to function.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // update current angle
        angle += speed * Time.deltaTime;

        // calculate new postion and update
        Vector3 newPosition = centerPosition + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
        transform.position = newPosition;
    }
}
