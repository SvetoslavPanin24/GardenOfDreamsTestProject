using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().interactable = false;
    }
    public void ChangeSliderValue(int value)
    {
        GetComponent<Slider>().value = value;
    }
}
