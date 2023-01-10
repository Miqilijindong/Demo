using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(OnMouseDown());
    }

    //IEnumerator OnMouseDown()//����������д��,ע���Сд
    //{
    //    //����������������ϵת��Ϊ��Ļ����ϵ
    //    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//��ά��������ת��Ļ����
    //    //�������Ļ����תΪ��ά���꣬���������λ�������֮��ľ���
    //    Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    //    while (Input.GetKey(KeyCode.Mouse0))
    //    {
    //        //�õ���������2ά����ϵλ��       
    //        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
    //        //����ǰ����2άλ��ת����3άλ�ã��ټ��������ƶ���
    //        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    //        //curPosition��������Ӧ�õ��ƶ���������transform��position����
    //        transform.position = curPosition;
    //        yield return new WaitForFixedUpdate(); //�������Ҫ��ѭ��ִ��
    //    }
    //}

    private void Update()
    {
        //ClickMouseMove();

        ClickMouseToMoveStuff();
    }

    /// <summary>
    /// UI�����
    /// </summary>
    public Camera theCamera;
    /// <summary>
    /// �ؼ�
    /// </summary>
    public GameObject Element;
    /// <summary>
    /// �ж��Ƿ������ؼ�
    /// </summary>
    public bool clicked = false;
    /// <summary>
    /// �Ƿ��пؼ�
    /// </summary>
    public bool isElement = false;
    /// <summary>
    /// ������
    /// </summary>
    Vector3 newWorldPos;
    Vector3 oldWorldPos;

    /// <summary>
    /// ��������ƶ�---�����������Ҫ�����Ƶ�Ч�����������ż̳�����࣬ʵ���������
    /// ���������ô˵�أ��о��е�֣��������ֽ��������Ϸ����������ģ�Ȼ��ذ��Tage������"Plane"��
    /// </summary>
    public virtual void ClickMouseToMoveStuff()
    {
        // ���������ʼ��������
        if (!clicked && Input.GetMouseButtonDown(0))
        {
            Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // ����ײ������ײ�壬������ı�ǩ������element
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "element")
            {
                Element = hit.collider.gameObject;
                Element.GetComponent<Collider>().enabled = false;
                isElement = true;
                clicked = true;
                oldWorldPos = hit.transform.position;
            }
        }

        // ���һֱ���ڰ���״̬����Ϊ��ק����
        if (clicked && Input.GetMouseButton(0))
        {
            // �����ק������Ϊelement
            if (isElement)
            {
                Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Plane" || hit.collider.tag == "element"))
                {

                    this.newWorldPos = hit.point;
                    newWorldPos.y += 1.5f;
                    Element.transform.position = newWorldPos;
                    Debug.Log(newWorldPos);
                }
            }
        }

        // �ɿ����
        if (clicked && Input.GetMouseButtonUp(0))
        {
            newWorldPos.y = oldWorldPos.y;
            Element.transform.position = newWorldPos;
            Element.GetComponent<Collider>().enabled = true;
            isElement = false;
            clicked = false;
            Element = null;
        }
    }

    /// <summary>
    /// ������Ϳ����ƶ���
    /// </summary>
    private void ClickMouseMove()
    {
        if (Input.GetMouseButton(0))
        {
            //����������������ϵת��Ϊ��Ļ����ϵ
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//��ά��������ת��Ļ����
            //�������Ļ����תΪ��ά���꣬���������λ�������֮��ľ���
            //Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            //�õ���������2ά����ϵλ��       
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(curScreenSpace);
            //����ǰ����2άλ��ת����3άλ�ã��ټ��������ƶ���
            Vector3 curPosition = vector3/* + offset*/;
            //curPosition��������Ӧ�õ��ƶ���������transform��position����
            transform.position = curPosition;
        }
    }
}
