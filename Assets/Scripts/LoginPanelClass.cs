using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelClass : MonoBehaviour
{
    public static LoginPanelClass instance;
    private Button returnButton;
    private Button loginButton;
    private InputField userName;
    private InputField passWord;

    private void Awake()
    {
        instance = this;

        returnButton = transform.Find("returnButton").GetComponent<Button>();
        loginButton = transform.Find("loginButton").GetComponent<Button>();
        userName = transform.Find("userName").GetComponent<InputField>();
        passWord = transform.Find("passWord").GetComponent<InputField>();

        returnButton.onClick.AddListener(returnButtonOnclick);
        loginButton.onClick.AddListener(loginButtonOnclick);

        gameObject.SetActive(false);
    }


    private void returnButtonOnclick()
    {
        // 返回主界面
        instance.gameObject.SetActive(false);
        MainPanelClass.instance.show();
    }

    private void loginButtonOnclick()
    {
        if (string.IsNullOrEmpty(userName.text)
            || string.IsNullOrEmpty(passWord.text))
        {
            FloatWindowClass.instance.show("账号或密码不能为空");
        }
        else
        {
            UserInfoClass userInfoClass = GameManger.instance.GetUserInfoClass(userName.text);
            if (userInfoClass != null && userInfoClass.passWord == passWord.text)
            {
                // 登录成功
                FloatWindowClass.instance.show("登录成功！");
                instance.gameObject.SetActive(false);
            } else
            {
                // 登录成功
                FloatWindowClass.instance.show("账号或密码错误！");
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
    }
}
