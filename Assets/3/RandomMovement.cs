using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // The minimum and maximum values for the transform's X and Y position
    private Vector2 minPosition = new Vector2(-5, -5);
    private Vector2 maxPosition = new Vector2(5, 5);

    // The speed at which the transform should move and rotate
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    // The target position for the transform
    private Vector3 targetPosition;

    // A flag to indicate whether the transform has reached the target position
    private bool hasReachedTarget = false;

    void Update()
    {
        // If the transform has not reached the target position yet, interpolate its position towards the target position
        if (!hasReachedTarget)
        {
            InterpolatePosition();

            // If the transform is close enough to the target position, set the hasReachedTarget flag to true
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                hasReachedTarget = true;
            }
        }
        // If the transform has reached the target position, generate a new target position and reset the hasReachedTarget flag
        else
        {
            GenerateNewTargetPosition();
            hasReachedTarget = false;
        }

        // Rotate the transform around the Z axis using the Time.deltaTime value to ensure a consistent rotation speed
        RotateTransform();
    }

    // Interpolates the transform's position towards the target position
    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
    }

    // Generates a new target position for the transform
    private void GenerateNewTargetPosition()
    {
        float x = Random.Range(minPosition.x, maxPosition.x);
        float y = Random.Range(minPosition.y, maxPosition.y);
        targetPosition = new Vector3(x, y, transform.position.z);
    }

    // Rotates the transform around the Z axis at a constant speed
    private void RotateTransform()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
