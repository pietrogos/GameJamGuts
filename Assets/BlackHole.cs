using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    // Variables for attracting objects
    public float pullForce = 10f;

    void OnTriggerStay(Collider other)
    {
        // Attract objects towards the black hole
        Vector3 direction = transform.position - other.transform.position;
        other.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForce);
    }
}