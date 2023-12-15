using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    public float radius = 50f; // radius of spherical orbital motion
    public float speed = 50f; 
    private Vector3 currentDirection; // current move direction
    private float timeSinceLastChange = 0f; // The time elapsed since the last direction change
    public float directionChangeInterval = 3f; // direction change interval

    void Start()
    {
        RandomizeDirection();
    }

    void Update()
    {
        // update obj position per frame
        transform.position += currentDirection * speed * Time.deltaTime;
        timeSinceLastChange += Time.deltaTime;

        // Randomly change the direction of motion when the direction change interval is reached
        if (timeSinceLastChange >= directionChangeInterval)
        {
            RandomizeDirection();
            timeSinceLastChange = 0f;
        }

        // If the object is too far from the center, bring it back within the radius distance
        if ((transform.position - transform.parent.position).magnitude > radius)
        {
            Vector3 directionToCenter = (transform.parent.position - transform.position).normalized;
            transform.position += directionToCenter * speed * Time.deltaTime;
        }
    }

    void RandomizeDirection()
    {
        // Randomly generate new spherical orbital motion directions
        currentDirection = Random.onUnitSphere;
    }
}
