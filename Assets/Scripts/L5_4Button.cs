using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class L5_4Button : MonoBehaviour
{
    private Button button;
    public GameObject DirectionalLight;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(changeLight);
    }

    void changeLight()
    {
        if (DirectionalLight.transform.GetComponent<Light>().isActiveAndEnabled) {
            DirectionalLight.SetActive(false);
        } else
        {
            DirectionalLight.SetActive(true); 
        }
    }
}
