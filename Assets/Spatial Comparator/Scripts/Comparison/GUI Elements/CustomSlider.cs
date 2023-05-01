using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueIndicator;
    public float sliderValue;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    void OnDisabled() { slider.onValueChanged.RemoveAllListeners(); }
    void OnDestroy() { slider.onValueChanged.RemoveAllListeners(); }

    private void OnValueChanged()
    {
        sliderValue = slider.value;
        valueIndicator.text = sliderValue.ToString();
    }
}

