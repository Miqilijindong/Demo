
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇摆动画---个人感觉这个会比较适合用于镜头震动或者关卡陷阱要触发的时候，但是要记住iTween.Stop();的话，是直接停止，保留当前的状态，之前的位置不会保留
/// </summary>
public class ITweenTest : MonoBehaviour
{
    private void Awake()
    {
        // 获取平方根
        Debug.Log(Mathf.Sqrt(3));
    }

    void Start()
    {
        //键值对儿的形式保存iTween所用到的参数  
        Hashtable args = new Hashtable();
        // 是在源数据基础上+-=这样子的
        // 比如scale = (1, 1, 1); 如果amount = (1, 0, 0); 那么就会在 (0, 1, 1) - (2, 1, 1)之间波动

        //摇摆的幅度  
        args.Add("amount", new Vector3(1, 1, 1));  
        //args.Add("x", 20);  
        //args.Add("y", 5);
        //args.Add("z", 2);  

        //是世界坐标系还是局部坐标系  
        args.Add("islocal", true);
        //游戏对象是否将面向其方向  
        args.Add("orienttopath", true);
        //面朝的对象  
        //args.Add("looktarget", new Vector3(1, 1, 1));  
        //args.Add("looktime", 5.0f);  

        //动画的整体时间。如果与speed共存那么优先speed  
        //args.Add("time", 1f);
        //延迟执行时间  
        //args.Add("delay", 0.1f);  

        //三个循环类型 none loop pingPong (一般 循环 来回)    
        //args.Add("loopType", "none");  
        //args.Add("loopType", iTween.LoopType.loop);
        //args.Add("loopType", iTween.LoopType.pingPong);


        //处理动画过程中的事件。  
        //开始动画时调用AnimationStart方法，5.0表示它的参数  
        args.Add("onstart", "AnimationStart");
        args.Add("onstartparams", 5.0f);
        //设置接受方法的对象，默认是自身接受，这里也可以改成别的对象接受，  
        //那么就得在接收对象的脚本中实现AnimationStart方法。  
        args.Add("onstarttarget", gameObject);


        //动画结束时调用，参数和上面类似  
        args.Add("oncomplete", "AnimationEnd");
        args.Add("oncompleteparams", "end");
        args.Add("oncompletetarget", gameObject);

        //动画中调用，参数和上面类似  
        args.Add("onupdate", "AnimationUpdate");
        args.Add("onupdatetarget", gameObject);
        args.Add("onupdateparams", true);

        //iTween.ShakeScale(gameObject, args);
        iTween.ShakePosition(gameObject, args);

    }


    //动画开始时调用  
    void AnimationStart(float f)
    {
        Debug.Log("start :" + f);
    }
    //动画结束时调用  
    void AnimationEnd(string f)
    {
        Debug.Log("end : " + f);

    }

    float i;
    //动画中调用  
    void AnimationUpdate(bool f)
    {
        Debug.Log(i);
        i = 0;
        //Debug.Log("update :" + f);

    }

    private void OnMouseDown()
    {
        iTween.Stop();
    }
}
