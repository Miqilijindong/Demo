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
        //����������������ϵת��Ϊ��Ļ����ϵ
        Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//��ά��������ת��Ļ����
        //�������Ļ����תΪ��ά���꣬���������λ�������֮��ľ���
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //�õ���������2ά����ϵλ��       
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //����ǰ����2άλ��ת����3άλ�ã��ټ��������ƶ���
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //curPosition��������Ӧ�õ��ƶ���������transform��position����
            transform.position = curPosition;
        }
    }
}
