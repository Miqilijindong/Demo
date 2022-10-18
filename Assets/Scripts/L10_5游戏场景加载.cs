using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 在file->build Setting里面设置
 */
public class L10_5游戏场景加载 : MonoBehaviour
{
    public 
    void Start()
    {
        // 加载场景---int:场景编号、string:场景名称
        SceneManager.LoadScene("L10_4数据存档");
        // 保留游戏物体，切换场景时不摧毁
        GameObject.DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
