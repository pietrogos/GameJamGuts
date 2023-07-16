using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlanetPowerUp : MonoBehaviour
{
    public GameObject fireAuraPrefab;
    public float duration = 4f;

    public void Activate(GameObject player)
    {
        // Instantiate FireAura and make it a child of the player
        GameObject fireAura = Instantiate(fireAuraPrefab, player.transform.position, Quaternion.identity);
        fireAura.layer = LayerMask.NameToLayer("Aura");
        fireAura.transform.parent = player.transform;
        fireAura.transform.localScale = player.transform.localScale * 3;

        // Destroy the FireAura after the duration
        Destroy(fireAura, duration);
    }
}