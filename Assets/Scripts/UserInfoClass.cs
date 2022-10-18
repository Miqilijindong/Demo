using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoClass
{
    public string userName;
    public string passWord;
    public bool isMan;

    public UserInfoClass(string userName, string passWord, bool isMan)
    {
        this.userName = userName;
        this.passWord = passWord;
        this.isMan = isMan;
    }

    /**
     * 这里不能这样哦，因为这个类没有挂在任何组件上，导致Awake()没有执行
     */
    private void Awake()
    {
    }
}

class GameManger
{
    public List<UserInfoClass> userInfos = new List<UserInfoClass>();
    private static GameManger _instance;
    public static GameManger instance
    {
        get
        {
            if (_instance == null) _instance = new GameManger();
            return _instance;
        }
    }


    public void addUserInfos(UserInfoClass userInfo)
    {
        instance.userInfos.Add(userInfo);
    }

    public UserInfoClass GetUserInfoClass(string userName) 
    {
        for (int i = 0; i < userInfos.Count; i++)
        {
            if (userInfos[i].userName == userName)
            {
                return userInfos[i];
            }
        }
        return null;
    }
}