using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_10协程程序Class : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Demo());
        //Coroutine coroutine = StartCoroutine(Demo2());
        //StartCoroutine(Demo3(coroutine));
        // 停止所有协程函数
        //StopAllCoroutines();
        //StopCoroutine()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetNum()
    {
        return 0;
    }

    /**
     * 协程函数
     */   
    public IEnumerator Demo()
    {
        Debug.Log("协程函数执行开始");
        // 两秒后执行 
        yield return new WaitForSeconds(2f);
        Debug.Log("协程函数执行结束");


        yield return null; // 下一帧执行
        Debug.Log("yield return null = 下一帧执行");
    }

    /**
     * 协程函数
     */
    public IEnumerator Demo2()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f); 
            transform.Rotate(new Vector3(5, 0, 0));
        }
    }

    /**
     * 协程函数
     */
    public IEnumerator Demo3(Coroutine coroutine)
    {
        yield return new WaitForSeconds(5f);
        StopCoroutine(coroutine);
    }
}
