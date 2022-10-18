using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 多边形
 */
public class Polygon 
{
    public int Length;
    public int Width;
    public string color;
    public string name;

    /**
     * 打印参数
     */
    public void showValue ()
    {
        Debug.Log("名字叫:" + name + "的，长度是:" + Length + ", 宽度是:" + Width + ", 颜色是:" + color);

    }

    /*
     * 虚函数---求面积
     */
    public virtual void getArea()
    {

    }
}

/**
 * 三角形
 */
class Trigon : Polygon
{
    /**
     * 重写
     */
    public override void getArea()
    {
        Debug.Log("面积等于:" + Length * Width / 2.0);
    }

    /**
     * 打印参数
     */
    public void showValue(string a)
    {
        Debug.Log("名字叫:" + name + "的，长度是:" + Length + ", 宽度是:" + Width + ", 颜色是:" + color);

    }
}

/**
 * 矩形
 */
class Rectangle : Polygon
{
    public override void getArea()
    {
        Debug.Log("面积等于:" + Length * Width);
    }

    /**
     * 打印参数
     */
    public void showValue(int a)
    {
        Debug.Log("名字叫:" + name + "的，长度是:" + Length + ", 宽度是:" + Width + ", 颜色是:" + color);

    }
}

public class Extern : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Trigon trigon = new Trigon();
        trigon.color = "黄色";
        trigon.name = "三角形";
        trigon.Length = 3;
        trigon.Width = 4;
        //Debug.Log("名字叫:" + trigon.name + "的，长度是:" + trigon.Length + ", 宽度是:" + trigon.Width + ", 颜色是:" + trigon.color);
        trigon.showValue();
        trigon.getArea();

        Rectangle rectangle = new Rectangle();
        rectangle.color = "绿色";
        rectangle.name = "矩形";
        rectangle.Length = 3;
        rectangle.Width = 4;
        //Debug.Log("名字叫:" + rectangle.name + "的，长度是:" + rectangle.Length + ", 宽度是:" + rectangle.Width + ", 颜色是:" + rectangle.color);
        rectangle.showValue();
        rectangle.getArea();

        // 多态子类赋值到父类
        Polygon test1 = trigon;
        Polygon test2 = rectangle;
        // 这个就是运行时多态
        test1.showValue();
        test2.showValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
