using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5_10ScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScrollRect scrollRect = transform.GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(scrollRectOnValueChange);
    }
    private void Update()
    {
        //Slider slider = transform.GetComponent<Slider>();
        //Debug.Log(slider.value);
    }

    public void scrollRectOnValueChange(Vector2 value)
    {
        Debug.Log(value);
    }
}
