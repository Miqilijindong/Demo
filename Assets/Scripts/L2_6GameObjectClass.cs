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
        Debug.Log("B.activeInHierarchy(��ʾ״̬):" + B.activeInHierarchy);

        // ��ѯ��Ϸ����
        GameObject C = GameObject.Find("C");
        Debug.Log("��������:" + C.transform.localPosition);
        Debug.Log("�������:" + C.transform.position);

        Debug.Log("B.tag = " + B.tag);

        // ��ȡ��Ϸ�������ϵ������T������ǲ�ѯ����
        Transform tempTransform = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<GameObject>();// �����Ǵ���ģ���Ȼ���벻�ᱨ�����Ƿ���ֵ��null
        Debug.Log(tempTransform.position);
        // ������ʾ״̬
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
