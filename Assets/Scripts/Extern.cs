using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �����
 */
public class Polygon 
{
    public int Length;
    public int Width;
    public string color;
    public string name;

    /**
     * ��ӡ����
     */
    public void showValue ()
    {
        Debug.Log("���ֽ�:" + name + "�ģ�������:" + Length + ", �����:" + Width + ", ��ɫ��:" + color);

    }

    /*
     * �麯��---�����
     */
    public virtual void getArea()
    {

    }
}

/**
 * ������
 */
class Trigon : Polygon
{
    /**
     * ��д
     */
    public override void getArea()
    {
        Debug.Log("�������:" + Length * Width / 2.0);
    }

    /**
     * ��ӡ����
     */
    public void showValue(string a)
    {
        Debug.Log("���ֽ�:" + name + "�ģ�������:" + Length + ", �����:" + Width + ", ��ɫ��:" + color);

    }
}

/**
 * ����
 */
class Rectangle : Polygon
{
    public override void getArea()
    {
        Debug.Log("�������:" + Length * Width);
    }

    /**
     * ��ӡ����
     */
    public void showValue(int a)
    {
        Debug.Log("���ֽ�:" + name + "�ģ�������:" + Length + ", �����:" + Width + ", ��ɫ��:" + color);

    }
}

public class Extern : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Trigon trigon = new Trigon();
        trigon.color = "��ɫ";
        trigon.name = "������";
        trigon.Length = 3;
        trigon.Width = 4;
        //Debug.Log("���ֽ�:" + trigon.name + "�ģ�������:" + trigon.Length + ", �����:" + trigon.Width + ", ��ɫ��:" + trigon.color);
        trigon.showValue();
        trigon.getArea();

        Rectangle rectangle = new Rectangle();
        rectangle.color = "��ɫ";
        rectangle.name = "����";
        rectangle.Length = 3;
        rectangle.Width = 4;
        //Debug.Log("���ֽ�:" + rectangle.name + "�ģ�������:" + rectangle.Length + ", �����:" + rectangle.Width + ", ��ɫ��:" + rectangle.color);
        rectangle.showValue();
        rectangle.getArea();

        // ��̬���ำֵ������
        Polygon test1 = trigon;
        Polygon test2 = rectangle;
        // �����������ʱ��̬
        test1.showValue();
        test2.showValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
