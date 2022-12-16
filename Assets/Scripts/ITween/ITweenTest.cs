
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ҡ�ڶ���---���˸о������Ƚ��ʺ����ھ�ͷ�𶯻��߹ؿ�����Ҫ������ʱ�򣬵���Ҫ��סiTween.Stop();�Ļ�����ֱ��ֹͣ��������ǰ��״̬��֮ǰ��λ�ò��ᱣ��
/// </summary>
public class ITweenTest : MonoBehaviour
{
    private void Awake()
    {
        // ��ȡƽ����
        Debug.Log(Mathf.Sqrt(3));
    }

    void Start()
    {
        //��ֵ�Զ�����ʽ����iTween���õ��Ĳ���  
        Hashtable args = new Hashtable();
        // ����Դ���ݻ�����+-=�����ӵ�
        // ����scale = (1, 1, 1); ���amount = (1, 0, 0); ��ô�ͻ��� (0, 1, 1) - (2, 1, 1)֮�䲨��

        //ҡ�ڵķ���  
        args.Add("amount", new Vector3(1, 1, 1));  
        //args.Add("x", 20);  
        //args.Add("y", 5);
        //args.Add("z", 2);  

        //����������ϵ���Ǿֲ�����ϵ  
        args.Add("islocal", true);
        //��Ϸ�����Ƿ������䷽��  
        args.Add("orienttopath", true);
        //�泯�Ķ���  
        //args.Add("looktarget", new Vector3(1, 1, 1));  
        //args.Add("looktime", 5.0f);  

        //����������ʱ�䡣�����speed������ô����speed  
        //args.Add("time", 1f);
        //�ӳ�ִ��ʱ��  
        //args.Add("delay", 0.1f);  

        //����ѭ������ none loop pingPong (һ�� ѭ�� ����)    
        //args.Add("loopType", "none");  
        //args.Add("loopType", iTween.LoopType.loop);
        //args.Add("loopType", iTween.LoopType.pingPong);


        //�����������е��¼���  
        //��ʼ����ʱ����AnimationStart������5.0��ʾ���Ĳ���  
        args.Add("onstart", "AnimationStart");
        args.Add("onstartparams", 5.0f);
        //���ý��ܷ����Ķ���Ĭ����������ܣ�����Ҳ���Ըĳɱ�Ķ�����ܣ�  
        //��ô�͵��ڽ��ն���Ľű���ʵ��AnimationStart������  
        args.Add("onstarttarget", gameObject);


        //��������ʱ���ã���������������  
        args.Add("oncomplete", "AnimationEnd");
        args.Add("oncompleteparams", "end");
        args.Add("oncompletetarget", gameObject);

        //�����е��ã���������������  
        args.Add("onupdate", "AnimationUpdate");
        args.Add("onupdatetarget", gameObject);
        args.Add("onupdateparams", true);

        //iTween.ShakeScale(gameObject, args);
        iTween.ShakePosition(gameObject, args);

    }


    //������ʼʱ����  
    void AnimationStart(float f)
    {
        Debug.Log("start :" + f);
    }
    //��������ʱ����  
    void AnimationEnd(string f)
    {
        Debug.Log("end : " + f);

    }

    float i;
    //�����е���  
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
