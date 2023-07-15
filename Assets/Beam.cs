using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float damage = 10f; // Amount of hunger to reduce
    public float lifeTime = 5f; // Time before the beam disappears

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Reduce the player's hunger
            other.gameObject.GetComponent<Devourer>().DecreaseHunger(damage);

            // Destroy the beam
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("IceAura"))
        {
            // If the beam hits an IceAura, slow down the beam
            GetComponent<Rigidbody>().velocity *= 0.7f;
        }
    }
}