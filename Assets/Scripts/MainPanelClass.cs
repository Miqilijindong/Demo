using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelClass : MonoBehaviour
{
    private Button loginButton;
    private Button registerButton;
    public static MainPanelClass instance;
    private void Awake()
    {
        instance = this;
        loginButton = transform.Find("loginButton").GetComponent<Button>();
        registerButton = transform.Find("registerButton").GetComponent<Button>();

        loginButton.onClick.AddListener(loginButtonOnclick);
        registerButton.onClick.AddListener(registerButtonOnclick);

        show();
    }

    private void loginButtonOnclick()
    {
        // �����¼����
        instance.gameObject.SetActive(false);
        LoginPanelClass.instance.show();
    }
    private void registerButtonOnclick()
    {
        // ����ע�����
        instance.gameObject.SetActive(false);
        RegisterPanelClass.instance.show();
    }

    public void show()
    {
        gameObject.SetActive(true);
    }
}
