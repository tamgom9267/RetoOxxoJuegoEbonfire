using UnityEngine;

public class LabEnemyBehaviour : MonoBehaviour
{
    public float velocity = 2f; // Speed of the enemy
    public float chaseDistance = 5f; // Distance at which the enemy starts chasing
    private Transform player; // Reference to the player

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= chaseDistance)
            {
                // Move towards the player when within chase distance
                transform.position = Vector3.MoveTowards(transform.position, player.position, velocity * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (LabGameControl.Instance != null) // Ensure Instance is set
            {
                LabGameControl.Instance.DeductPoints(); // Deduct 100 points
            }

            Destroy(gameObject); // Destroy enemy
        }
    }
}




