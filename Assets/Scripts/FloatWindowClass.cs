using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatWindowClass : MonoBehaviour
{
    public static FloatWindowClass instance;
    private Text text;
    private Button returnButton;
    private void Awake()
    {
        instance = this;
        text = transform.Find("Text").GetComponent<Text>();
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        returnButton.onClick.AddListener(onClickReturnButton);

        gameObject.SetActive(false);
    }

    public void show(string showInfo)
    {
        text.text = showInfo;
        gameObject.SetActive(true);
    }

    public void onClickReturnButton()
    {
        gameObject.SetActive(false);
        MainPanelClass.instance.show();
    }
}
