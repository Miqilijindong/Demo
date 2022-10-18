using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_6GameObjectClass : MonoBehaviour
{
    public GameObject B;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(B);
        Debug.Log(B.transform);
        Debug.Log(gameObject);
        Debug.Log(gameObject.transform);
        Debug.Log("B.activeInHierarchy(显示状态):" + B.activeInHierarchy);

        // 查询游戏物体
        GameObject C = GameObject.Find("C");
        Debug.Log("本地坐标:" + C.transform.localPosition);
        Debug.Log("相对坐标:" + C.transform.position);

        Debug.Log("B.tag = " + B.tag);

        // 获取游戏物体身上的组件，T代表的是查询类型
        Transform tempTransform = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<GameObject>();// 这样是错误的，虽然代码不会报错，但是返回值是null
        Debug.Log(tempTransform.position);
        // 设置显示状态
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
