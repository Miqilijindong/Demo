using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_5TransformClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        #region ����
        Debug.Log("childCount:����������:" + transform.childCount);
        Debug.Log("parent:�������TransForm���:" + transform.parent);
        Debug.Log("root:��߼�������:" + transform.root);
        Debug.Log("root:position(��ǰ����):" + transform.position);
        Debug.Log("root:rotation(��ǰ��ת):" + transform.rotation);
        Debug.Log("root:eulerAngles():" + transform.eulerAngles);
        Debug.Log("root:localScale(��ǰ����):" + transform.localScale);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region ����
        // ����������
        Debug.Log(transform.Find("C"));
        // �ƶ�---����һ
        transform.Translate(new Vector3(0.01f, 0, 0));
        // �ƶ�---������
        transform.position += new Vector3(0, 0.01f, 0);
        Transform TFC = transform.Find("C");
        TFC.Rotate(new Vector3(2, 0, 0));
        transform.LookAt(TFC);
        
        #endregion
    }
}
