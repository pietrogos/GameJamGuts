using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{
    public Devourer devourer;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = devourer.maxHunger;
        slider.value = devourer.hunger;
    }
}
