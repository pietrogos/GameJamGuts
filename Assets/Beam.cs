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

             Devourer devourer = other.gameObject.GetComponent<Devourer>();
            
            // Check if the player has an active Shield
            if (devourer.transform.Find("Shield(Clone)") != null)
            {
                // If the Shield is active, don't do damage and destroy the beam
                Destroy(gameObject);
                return;
            }

            // Reduce the player's hunger
            devourer.DecreaseHunger(damage);

            // Destroy the beam
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("IceAura"))
        {
            // If the beam hits an IceAura, slow down the beam
            GetComponent<Rigidbody>().velocity *= 0.3f;
        }
        else if (other.gameObject.CompareTag("FireAura"))
        {
            // If the beam hits a FireAura, destroy the beam
            Destroy(gameObject);
        }
    }
}