using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    // Variables for freezing enemies
    public float freezeDuration = 10f;
    public float expansionRate = 1f; // How quickly the wave expands
    public float maxSize = 5f; // Maximum size of the wave

    private void Start()
    {
        StartCoroutine(Expand());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Assuming the enemy has a script that can freeze it
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Freeze(freezeDuration);
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