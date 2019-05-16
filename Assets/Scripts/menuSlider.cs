using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuSlider : MonoBehaviour {
    public enum SliderType {
        Lenth, Width
    }
    public SliderType sliderType;
    public Text displayNumber;
    private Slider thisSlider;
    

    // Use this for initialization
    void Start ()
    {
        thisSlider = this.GetComponent<Slider>();
        if (sliderType == SliderType.Lenth)
        {
            thisSlider.value = gameMaster.length;
            displayNumber.text = gameMaster.length.ToString();
        }
        else if (sliderType == SliderType.Width)
        {
            thisSlider.value = gameMaster.width;
            displayNumber.text = gameMaster.width.ToString();
        }
    }

    public void OnValueChange() {
        displayNumber.text = thisSlider.value.ToString();
        if (sliderType == SliderType.Lenth)
            gameMaster.length = (int)thisSlider.value;
        else
            gameMaster.width = (int)thisSlider.value;
    }
}
