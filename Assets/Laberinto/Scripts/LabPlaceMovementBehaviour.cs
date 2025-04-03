using UnityEngine;

public class LabPlaceMovementBehaviour : MonoBehaviour
{
    public Transform pointA; // First position
    public Transform pointB; // Second position
    public float speed = 2f; // Movement speed

    private Vector3 targetPosition;
    private bool facingRight = true;

    void Start()
    {
        if (pointA != null && pointB != null)
        {
            targetPosition = pointB.position; // Start moving towards point B
        }
    }

    void Update()
    {
        if (pointA != null && pointB != null)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if reached target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Switch target position
                targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip character by inverting X scale
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LabGameControl.Instance != null) // Ensure Instance is set
            {
                LabGameControl.Instance.DeductPoints(); // Deduct points
            }
        }
    }
}

