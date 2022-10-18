using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5_6Toggle : MonoBehaviour
{
    private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(onValueChange);
    }

    private void onValueChange(bool bl)
    {
        Debug.Log("onValueChange" + bl);
    }
}
