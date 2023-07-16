using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devourer : MonoBehaviour
{
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float depletionPercent = 0.01f;      
    private float depletionRate;

    public void DecreaseHunger(float amount)
    {
        hunger -= amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current depletion rate
        depletionRate = maxHunger * depletionPercent;
        hunger = Mathf.Max(hunger - depletionRate * Time.deltaTime, 0);

        if (hunger <= 0)
        {
            // Game over
            Debug.Log("Game Over");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Beam" && transform.localScale.x >= other.gameObject.transform.localScale.x)
        {
            Destroy(other.gameObject);
            float growthRate = other.gameObject.transform.localScale.x * 0.02f;
            transform.localScale = transform.localScale + new Vector3(growthRate, growthRate, growthRate);

            // Check for power-up
            IcePlanetPowerUp icePowerUp = other.gameObject.GetComponent<IcePlanetPowerUp>();
            if (icePowerUp != null)
            {
                icePowerUp.Activate(gameObject);
            }

            FirePlanetPowerUp firePowerUp = other.gameObject.GetComponent<FirePlanetPowerUp>();
            if (firePowerUp != null)
            {
                firePowerUp.Activate(gameObject);
            }

            MetalPlanetPowerUp metalPlanetPowerUp = other.gameObject.GetComponent<MetalPlanetPowerUp>();
            if (metalPlanetPowerUp != null)
            {
                metalPlanetPowerUp.Activate(gameObject);
            }

            maxHunger += other.gameObject.transform.localScale.x * 0.5f;
            hunger = maxHunger; // Refill hunger when devouring a planet
        }
    }
}