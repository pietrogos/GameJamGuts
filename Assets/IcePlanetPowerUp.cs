using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanetPowerUp : MonoBehaviour
{
    public GameObject iceAuraPrefab;
    public float duration = 12f;

    public void Activate(GameObject player)
    {
        // Instantiate IceAura and make it a child of the player
        GameObject iceAura = Instantiate(iceAuraPrefab, player.transform.position, Quaternion.identity);
        iceAura.transform.parent = player.transform;

        // Scale the aura to be slightly larger than the player
        iceAura.transform.localScale = player.transform.localScale * 6;

        // Destroy the IceAura after the duration
        Destroy(iceAura, duration);
    }
}
