using UnityEngine;

public class Ball : MonoBehaviour
{

    // The initial speed of the ball
    [Range(1, 100)] [Tooltip("The initial speed of the ball. Valid values are between 1 and 100.")] [SerializeField]
    private float initialSpeed = 10f;


    // The force of gravity applied to the ball
    [SerializeField] private Vector2 gravity = new Vector2(0, -9.81f);

    // The percentage by which to reduce the ball's speed when it collides
    // with a vertical wall
    [Range(0, 1)]
    [Tooltip(
        "The percentage by which to reduce the ball's speed when it collides with a vertical wall. Valid values are between 0 and 1.")]
    [SerializeField]
    private float wallSpeedReduction = 0.7f;

    // The percentage by which to reduce the ball's speed when it collides
    // with the floor
    [Range(0, 1)]
    [Tooltip(
        "The percentage by which to reduce the ball's speed when it collides with the floor. Valid values are between 0 and 1.")]
    [SerializeField]
    private float floorSpeedReduction = 0.5f;

    // The speed to launch the ball at when the user clicks
    [Range(1, 100)]
    [Tooltip("The speed to launch the ball at when the user clicks. Valid values are between 1 and 100.")]
    [SerializeField]
    private float launchSpeed = 20f;

    // The layer mask to use for raycasting
    [SerializeField] private LayerMask layerMask;

    private readonly Vector2 _initialDirection = Vector2.one;

    private Vector2 _direction;

    private float _speed = 10f;

    private float _radius;

    void Start()
    {
        // Set the initial direction to be up and to the right
        //direction = new Vector2(1, 1);

        // Get the ball's radius from its collider
        _radius = GetComponent<Collider2D>().bounds.extents.magnitude;

        // Set the initial direction of the ball
        _direction = _initialDirection;

        // Set the initial speed of the ball
        _speed = initialSpeed;

        // Get the ball's radius from its collider
        _radius = GetComponent<Collider2D>().bounds.extents.magnitude;
    }

    void Update()
    {
        // Wait for five seconds before simulating gravity
        if (Time.timeSinceLevelLoad < 2)
        {
            return;
        }

        UpdateGravity();
        HandleMouseInput();
        MoveBall();
        HandleCollisions();
    }


    private void UpdateGravity()
    {
        _direction += gravity * Time.deltaTime;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _direction = Random.insideUnitCircle.normalized;
            _direction = Vector2.ClampMagnitude(_direction, 1);
            _speed = launchSpeed;
            transform.position = Vector2.zero; // reset position to start position
        }
    }

    private void MoveBall()
    {
        // Move the ball in the direction it is facing at the specified speed
        Vector3 newPosition = transform.position + (Vector3)_direction * (_speed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void HandleCollisions()
    {
        // Move the ball in the direction it is facing at the specified speed
        Vector3 newPosition = transform.position + (Vector3)_direction * (_speed * Time.deltaTime);
        transform.position = newPosition;

        // Cast a ray from the ball's center in the direction it is moving,
        // with a length equal to the ball's radius
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, _radius, layerMask);

        // If the ray hits a collider, adjust the ball's position and direction
        // to simulate a collision
        // If the ray hits a collider, adjust the ball's position and direction
        // to simulate a collision
        if (hit.collider != null)
        {
            // Get the normal of the surface we collided with
            Vector2 normal = hit.normal;

            // If the normal is a vertical vector (i.e. the ball has hit a vertical wall),
            // reflect the ball's direction vector off of the wall and reduce its speed
            if (normal.x != 0)
            {
                // Calculate the new position of the ball so that it doesn't pass through the wall
                Vector3 nextPosition = transform.position + (Vector3)transform.up * (_speed * Time.deltaTime);
                transform.position = newPosition;

                // Reflect the ball's direction vector off of the wall and reduce its speed
                _direction = Vector2.Reflect(_direction, normal);
                _speed *= wallSpeedReduction;
            }
            // If the normal is a horizontal vector (i.e. the ball has hit the floor),
            // reflect the ball's direction vector off of the floor and reduce its speed
            else if (normal.y != 0)
            {
                _direction = Vector2.Reflect(_direction, normal);
                _speed *= floorSpeedReduction;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        // Set the color of the gizmos to green
        Gizmos.color = Color.green;

        // Draw a circle representing the ball's radius
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


    void OnDrawGizmosSelected()
    {
        // Set the color of the gizmos to blue
        Gizmos.color = Color.blue;

        // Draw a circle representing the ball's radius
        Gizmos.DrawWireSphere(transform.position, _radius);

        // Draw a label showing the ball's speed
        Gizmos.DrawIcon(transform.position + Vector3.up * 0.5f, "Speed: " + _speed);
    }
}

