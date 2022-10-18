using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_5TransformClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        #region 属性
        Debug.Log("childCount:子物体数量:" + transform.childCount);
        Debug.Log("parent:父物体的TransForm组件:" + transform.parent);
        Debug.Log("root:最高级别物体:" + transform.root);
        Debug.Log("root:position(当前坐标):" + transform.position);
        Debug.Log("root:rotation(当前旋转):" + transform.rotation);
        Debug.Log("root:eulerAngles():" + transform.eulerAngles);
        Debug.Log("root:localScale(当前缩放):" + transform.localScale);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region 方法
        // 查找子物体
        Debug.Log(transform.Find("C"));
        // 移动---方法一
        transform.Translate(new Vector3(0.01f, 0, 0));
        // 移动---方法二
        transform.position += new Vector3(0, 0.01f, 0);
        Transform TFC = transform.Find("C");
        TFC.Rotate(new Vector3(2, 0, 0));
        transform.LookAt(TFC);
        
        #endregion
    }
}
