using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 5f; // Adjust this to control the speed of the follower
    public float followDistance = 1f; // Distance at which the follower starts following the player

    private GameObject player;
    private bool isFollowing = false;

    void Start()
    {
        // Find the Player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check if player is not null and if Follower is following
        if (player != null && isFollowing)
        {
            // Calculate direction towards the player
            Vector3 direction = (player.transform.position - transform.position).normalized;

            // Move towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
        else if (player != null)
        {
            // Check if the player is within follow distance
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= followDistance)
            {
                // Start following the player
                isFollowing = true;
            }
        }
    }
}
