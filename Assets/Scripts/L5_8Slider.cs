using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5_8Slider : MonoBehaviour
{
    public float Fslider;
    // Start is called before the first frame update
    void Start()
    {
        Slider slider = transform.GetComponent<Slider>();
        slider.onValueChanged.AddListener(sliderOnValueChange);
    }
    private void Update()
    {
        //Slider slider = transform.GetComponent<Slider>();
        //Debug.Log(slider.value);
    }

    public void sliderOnValueChange(float value)
    {
        Debug.Log(value);
    }
}
