using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 5f; // Enemy speed
    public float stopDistance = 2f; // Distance at which enemy stops moving towards the player
    public float stopMargin = 0.1f; // Margin around stopDistance to prevent oscillations
    public float raycastDistance = 5f; // Distance of avoidance raycast
    public GameObject beamPrefab; // Beam prefab
    public float fireRate = 1f; // Fire rate in beams per second
    public float beamSpeed = 5f; // Speed of beams

    private float fireTimer = 0f; // Time since last fired

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calculate the direction towards the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Restrict movement to the XZ plane
        directionToPlayer.y = 0;

        // Avoidance
        if (Physics.Raycast(transform.position, directionToPlayer, raycastDistance))
        {
            // If there is a planet in the way, move perpendicularly to the direction of the player
            directionToPlayer += new Vector3(-directionToPlayer.z, 0, directionToPlayer.x);
        }

        // If the player is farther than stopDistance plus margin, move towards the player
        if (distanceToPlayer > stopDistance + stopMargin)
        {
            transform.position += directionToPlayer * speed * Time.deltaTime;
        }
        // If the player is closer than stopDistance minus margin, move away from the player
        else if (distanceToPlayer < stopDistance - stopMargin)
        {
            transform.position -= directionToPlayer * speed * Time.deltaTime;
        }

        // Fire at the player
        fireTimer += Time.deltaTime;
        if (fireTimer > 1f / fireRate)
        {
            fireTimer = 0f;
            GameObject beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beam.transform.LookAt(player.position);
            beam.GetComponent<Rigidbody>().velocity = beam.transform.forward * beamSpeed;
        }
    }
}