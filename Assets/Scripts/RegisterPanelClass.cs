using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanelClass : MonoBehaviour
{
    public static RegisterPanelClass instance;
    private Button returnButton;
    private Button registerButton;
    private Toggle manToggle;
    private InputField userName;
    private InputField passWord;
    private InputField rePassWord;
    private void Awake()
    {
        instance = this;
        returnButton = transform.Find("returnButton").GetComponent<Button>();
        registerButton = transform.Find("registerButton").GetComponent<Button>();
        manToggle = transform.Find("manToggle").GetComponent<Toggle>();
        userName = transform.Find("userName").GetComponent<InputField>();
        passWord = transform.Find("passWord").GetComponent<InputField>();
        rePassWord = transform.Find("rePassWord").GetComponent<InputField>();

        returnButton.onClick.AddListener(returnButtonOnclick);
        registerButton.onClick.AddListener(registerButtonOnclick);

        gameObject.SetActive(false);
    }
    private void returnButtonOnclick()
    {
        // ����������
        instance.gameObject.SetActive(false);
        MainPanelClass.instance.show();
    }

    private void registerButtonOnclick()
    {
        if(string.IsNullOrEmpty(userName.text)
            || string.IsNullOrEmpty(passWord.text)
            || string.IsNullOrEmpty(rePassWord.text))
        {
            FloatWindowClass.instance.show("�˺Ż����벻��Ϊ��");
        } else if(passWord.text != rePassWord.text)
        {
            FloatWindowClass.instance.show("���벻һ��");
        } else
        {
            UserInfoClass userInfo = new UserInfoClass(userName.text, passWord.text, manToggle.isOn);
            if (GameManger.instance.GetUserInfoClass(userInfo.userName) != null)
            {
                FloatWindowClass.instance.show("���˺��Ѵ���");
                return;
            } else
            {
                // ע��ɹ�
                instance.gameObject.SetActive(false);
                FloatWindowClass.instance.show("ע��ɹ�");
                GameManger.instance.addUserInfos(userInfo);
            }
        }
    }

    public void show()
    {
        instance.gameObject.SetActive(true);
        clear();
    }

    public void clear()
    {
        userName.text = "";
        passWord.text = "";
        rePassWord.text = "";
    }
}
