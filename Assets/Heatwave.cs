using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWave : MonoBehaviour
{
    // Variables for deactivating enemy systems
    public float deactivateDuration = 2.5f;
    public float expansionRate = 1f; // How quickly the wave expands
    public float maxSize = 5f; // Maximum size of the wave

    private void Start()
    {
        StartCoroutine(Expand());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyAI component and call its Deactivate function
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Deactivate(deactivateDuration);
            }
        }
    }

    IEnumerator Expand()
    {
        while (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(expansionRate, expansionRate, expansionRate) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject); // Destroy the wave after reaching max size
    }
}