using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devourer : MonoBehaviour
{
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float depletionPercent = 0.01f;      
    private float depletionRate;

    public GameObject iceAuraPrefab;
    public GameObject fireAuraPrefab;
    public GameObject metalAuraPrefab;
    public GameObject iceMetalAuraPrefab;
    public GameObject fireMetalAuraPrefab;

    public GameObject shockwavePrefab;
    public GameObject blackHolePrefab;
    public GameObject heatWavePrefab;

    // Keep track of the active power-ups
    private IcePlanetPowerUp activeIcePowerUp = null;
    private FirePlanetPowerUp activeFirePowerUp = null;
    private MetalPlanetPowerUp activeMetalPowerUp = null;

    private bool hasIceAura = false;
    private bool hasMetalAura = false;
    private bool hasFireAura = false;
    
    // Keep track of the aura game objects
    private GameObject activeIceAura = null;
    private GameObject activeFireAura = null;
    private GameObject activeMetalAura = null;
    private GameObject activeIceMetalAura = null;
    private GameObject activeFireMetalAura = null;


    public void DecreaseHunger(float amount)
    {
        hunger -= amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
    }

    void ActivateIcePowerUp(IcePlanetPowerUp powerUp)
    {
        Debug.Log("Ativou gelo");
        activeIceAura = Instantiate(iceAuraPrefab, transform.position, Quaternion.identity);
        activeIceAura.layer = LayerMask.NameToLayer("Aura");
        activeIceAura.transform.parent = transform;
        activeIceAura.transform.localScale = transform.localScale * 4;
        activeIceAura.tag = "IceAura";

        activeIcePowerUp = powerUp;
        hasIceAura = true;

        if(CheckCombos() == false)
        {
            CancelInvoke(nameof(DeactivateIcePowerUp));
            Invoke(nameof(DeactivateIcePowerUp), powerUp.duration);
        }
    }

    void ActivateFirePowerUp(FirePlanetPowerUp powerUp)
    {
        Debug.Log("Ativou fogo");
        activeFireAura = Instantiate(fireAuraPrefab, transform.position, Quaternion.identity);
        activeFireAura.layer = LayerMask.NameToLayer("Aura");
        activeFireAura.transform.parent = transform;
        activeFireAura.transform.localScale = transform.localScale * 4;
        activeFireAura.tag = "FireAura";

        activeFirePowerUp = powerUp;
        hasFireAura = true;

        if(CheckCombos() == false)
        {
            CancelInvoke(nameof(DeactivateFirePowerUp));
            Invoke(nameof(DeactivateFirePowerUp), powerUp.duration);
        }
    }

    void ActivateMetalPowerUp(MetalPlanetPowerUp powerUp)
    {
        Debug.Log("Ativou metal");
        activeMetalAura = Instantiate(metalAuraPrefab, transform.position, Quaternion.identity);
        activeMetalAura.layer = LayerMask.NameToLayer("Aura");
        activeMetalAura.transform.parent = transform;
        activeMetalAura.transform.localScale = transform.localScale * 4;
        activeMetalAura.tag = "MetalAura";

        activeMetalPowerUp = powerUp;
        hasMetalAura = true;

        if(CheckCombos() == false)
        {
            CancelInvoke(nameof(DeactivateMetalPowerUp));
            Invoke(nameof(DeactivateMetalPowerUp), powerUp.duration);
        }
    }

    void ExecuteIceFireCombo()
    {
        Debug.Log("Ativou ice fire");
        DeactivateIcePowerUp();
        DeactivateFirePowerUp();

        // Instantiate a shockwave that stuns enemies
        GameObject shockwave = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        
        // Set the shockwave's initial scale to the monster's scale
        shockwave.transform.localScale = transform.localScale;
    }

    void ExecuteFireMetalCombo()
    {
        Debug.Log("Ativou fire metal");
        DeactivateFirePowerUp();
        DeactivateMetalPowerUp();

        if (activeFireMetalAura == null)
        {
            activeFireMetalAura = Instantiate(fireMetalAuraPrefab, transform.position, Quaternion.identity);
            activeFireMetalAura.layer = LayerMask.NameToLayer("Aura");
            activeFireMetalAura.transform.parent = transform;
            activeFireMetalAura.transform.localScale = transform.localScale * 4;
            activeFireMetalAura.tag = "FireMetalAura";
        }

        // Instantiate a heatwave that deactivates enemy systems
        GameObject heatWave = Instantiate(heatWavePrefab, transform.position, Quaternion.identity);
        
        // Set the heatwave's initial scale to the monster's scale
        heatWave.transform.localScale = transform.localScale;

        // Destroy the heatWave after 6 seconds
        Invoke(nameof(DeactivateFireMetalCombo), 6f);
    }

    void ExecuteIceMetalCombo()
    {
        Debug.Log("Ativou ice metal");
        DeactivateIcePowerUp();
        DeactivateMetalPowerUp();

        if (activeIceMetalAura == null)
        {
            activeIceMetalAura = Instantiate(iceMetalAuraPrefab, transform.position, Quaternion.identity);
            activeIceMetalAura.layer = LayerMask.NameToLayer("Aura");
            activeIceMetalAura.transform.parent = transform;
            activeIceMetalAura.transform.localScale = transform.localScale * 4;
            activeIceMetalAura.tag = "IceMetalAura";
        }
        
        // Destroy the shield after 15 seconds
        Invoke(nameof(DeactivateIceMetalCombo), 15f);
    }

    void ExecuteTripleCombo()
    {
        Debug.Log("Ativou o caralho");
        DeactivateIcePowerUp();
        DeactivateFirePowerUp();
        DeactivateMetalPowerUp();

        // Instantiate a black hole that pulls in and destroys nearby objects
        GameObject blackHole = Instantiate(blackHolePrefab, transform.position, Quaternion.identity);

        // Assuming the blackHole object has a script that pulls in and destroys nearby objects

        // Destroy the blackHole after 3 seconds
        Destroy(blackHole, 3f);
    }

    void DeactivateFirePowerUp()
    {
        Debug.Log("Desativou o fogo");
        Destroy(activeFireAura);
        hasFireAura = false;
        activeFireAura = null;
        activeFirePowerUp = null;
    }

    void DeactivateIcePowerUp()
    {
        Debug.Log("Desativou o gelo");
        Destroy(activeIceAura);
        hasIceAura = false;
        activeIceAura = null;
        activeIcePowerUp = null;
    }

    void DeactivateMetalPowerUp()
    {
        Debug.Log("Desativou o metal");
        Destroy(activeMetalAura);
        hasMetalAura = false;
        activeMetalAura = null;
        activeMetalPowerUp = null;
    }

    void DeactivateFireMetalCombo()
    {
        Debug.Log("Desativou o firemetal");
        Destroy(activeFireMetalAura);
        activeFireMetalAura = null;
    }

    void DeactivateIceMetalCombo()
    {
        Debug.Log("Desativou o icemetal");
        Destroy(activeIceMetalAura);
        activeIceMetalAura = null;
    }

    bool CheckCombos()
    {
        // Fire and Metal combo
        if (hasFireAura && hasMetalAura)
        {
            ExecuteFireMetalCombo();
            return true;
        }
        // Ice and Metal combo
        else if (hasIceAura && hasMetalAura)
        {
            ExecuteIceMetalCombo();
            return true;
        }
        // Ice and Fire combo
        else if (hasIceAura && hasFireAura)
        {
            ExecuteIceFireCombo();
            return true;
        }
        // Ice, Fire and Metal combo
        else if (hasFireAura && hasIceAura && activeMetalPowerUp != null)
        {
            ExecuteTripleCombo();
            return true;
        }
        else
        {
            Debug.Log("Não tem combo");
            return false;
        } 
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
            float growthRate = other.gameObject.transform.localScale.x * 0.5f;
            transform.localScale = transform.localScale + new Vector3(growthRate, growthRate, growthRate);

            // Check for power-up and activate it
            IcePlanetPowerUp icePowerUp = other.gameObject.GetComponent<IcePlanetPowerUp>();
            if (icePowerUp != null)
            {
                ActivateIcePowerUp(icePowerUp);
            }

            FirePlanetPowerUp firePowerUp = other.gameObject.GetComponent<FirePlanetPowerUp>();
            if (firePowerUp != null)
            {
                ActivateFirePowerUp(firePowerUp);
            }

            MetalPlanetPowerUp metalPowerUp = other.gameObject.GetComponent<MetalPlanetPowerUp>();
            if (metalPowerUp != null)
            {
                ActivateMetalPowerUp(metalPowerUp);
            }

            maxHunger += other.gameObject.transform.localScale.x * 0.5f;
            hunger = maxHunger; // Refill hunger when devouring a planet
        }
    }
}