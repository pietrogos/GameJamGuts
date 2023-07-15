using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Beam"))
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

