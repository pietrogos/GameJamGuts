using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPlanetPowerUp : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float duration = 25f;

    public void Activate(GameObject player)
    {
        // Instantiate Shield and make it a child of the player
        GameObject shield = Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);
        shield.layer = LayerMask.NameToLayer("Aura");
        shield.transform.parent = player.transform;
        shield.transform.localScale = player.transform.localScale * 2;

        // Destroy the Shield after the duration
        Destroy(shield, duration);
    }
}
