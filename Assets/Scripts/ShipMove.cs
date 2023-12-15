using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 startingPosition;
    private float distanceTraveled = 0f; // distance moved

    void Start()
    {
        startingPosition = transform.position; // Record the initial position of the object
    }

    void Update()
    {
        // Move the object along the X-axis every frame
        float moveStep = speed * Time.deltaTime;
        transform.position -= Vector3.right * moveStep;
        distanceTraveled += moveStep;

        // Adjust the transparency of an object and its sub-objects
        UpdateTransparency(gameObject, distanceTraveled);

        if (distanceTraveled >= 2500f)
        {
            // Teleport back to origin and reset transparency
            transform.position = startingPosition;
            distanceTraveled = 0f;
            UpdateTransparency(gameObject, 0f);
        }
    }

    void UpdateTransparency(GameObject obj, float distance)
    {
        float alpha = Mathf.Clamp01(1f - (distance / 2500f));
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color newColor = new Color(1, 1, 1, alpha);
            renderer.material.color = newColor;
        }

        // Recursively adjust the transparency of all child objects
        foreach (Transform child in obj.transform)
        {
            UpdateTransparency(child.gameObject, distance);
        }
    }
}
