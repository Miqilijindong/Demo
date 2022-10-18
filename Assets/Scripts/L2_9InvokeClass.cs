using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_9InvokeClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // time秒后执行"methodName"方法
        //Invoke("Demo", 1);
        // time秒后执行"methodName"方法后，每repeatRate秒后执行一次
        InvokeRepeating("Demo", 1, 1);
        // 取消调用CancelInvoke()---如果无参就是全部取消
        //CancelInvoke();
        // 5秒后取消循环
        Invoke("CancelInvoke", 5);
    }

    void Demo()
    {
        Debug.Log("执行Demo");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
