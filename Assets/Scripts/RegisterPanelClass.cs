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
        // 返回主界面
        instance.gameObject.SetActive(false);
        MainPanelClass.instance.show();
    }

    private void registerButtonOnclick()
    {
        if(string.IsNullOrEmpty(userName.text)
            || string.IsNullOrEmpty(passWord.text)
            || string.IsNullOrEmpty(rePassWord.text))
        {
            FloatWindowClass.instance.show("账号或密码不能为空");
        } else if(passWord.text != rePassWord.text)
        {
            FloatWindowClass.instance.show("密码不一致");
        } else
        {
            UserInfoClass userInfo = new UserInfoClass(userName.text, passWord.text, manToggle.isOn);
            if (GameManger.instance.GetUserInfoClass(userInfo.userName) != null)
            {
                FloatWindowClass.instance.show("此账号已存在");
                return;
            } else
            {
                // 注册成功
                instance.gameObject.SetActive(false);
                FloatWindowClass.instance.show("注册成功");
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
