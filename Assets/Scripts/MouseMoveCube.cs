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

    //IEnumerator OnMouseDown()//方法名不能写错,注意大小写
    //{
    //    //将物体由世界坐标系转换为屏幕坐标系
    //    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
    //    //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
    //    Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    //    while (Input.GetKey(KeyCode.Mouse0))
    //    {
    //        //得到现在鼠标的2维坐标系位置       
    //        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
    //        //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
    //        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    //        //curPosition就是物体应该的移动向量赋给transform的position属性
    //        transform.position = curPosition;
    //        yield return new WaitForFixedUpdate(); //这个很重要，循环执行
    //    }
    //}

    private void Update()
    {
        //ClickMouseMove();

        ClickMouseToMoveStuff();
    }

    /// <summary>
    /// UI摄像机
    /// </summary>
    public Camera theCamera;
    /// <summary>
    /// 控件
    /// </summary>
    public GameObject Element;
    /// <summary>
    /// 判断是否点击过控件
    /// </summary>
    public bool clicked = false;
    /// <summary>
    /// 是否有控件
    /// </summary>
    public bool isElement = false;
    /// <summary>
    /// 新坐标
    /// </summary>
    Vector3 newWorldPos;
    Vector3 oldWorldPos;

    /// <summary>
    /// 点击物体移动---其他类如果需要有类似的效果，可以试着继承这个类，实现这个方法
    /// 这个方法怎么说呢，感觉有点怪，像是那种建筑类的游戏或者塔防类的，然后地板的Tage必须是"Plane"，
    /// </summary>
    public virtual void ClickMouseToMoveStuff()
    {
        // 按下左键开始发出射线
        if (!clicked && Input.GetMouseButtonDown(0))
        {
            Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // 射线撞击到碰撞体，且物体的标签必须是element
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "element")
            {
                Element = hit.collider.gameObject;
                Element.GetComponent<Collider>().enabled = false;
                isElement = true;
                clicked = true;
                oldWorldPos = hit.transform.position;
            }
        }

        // 左键一直处于按下状态，即为拖拽过程
        if (clicked && Input.GetMouseButton(0))
        {
            // 如果拖拽物体标记为element
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

        // 松开左键
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
    /// 点击鼠标就可以移动的
    /// </summary>
    private void ClickMouseMove()
    {
        if (Input.GetMouseButton(0))
        {
            //将物体由世界坐标系转换为屏幕坐标系
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
            //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
            //Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            //得到现在鼠标的2维坐标系位置       
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(curScreenSpace);
            //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
            Vector3 curPosition = vector3/* + offset*/;
            //curPosition就是物体应该的移动向量赋给transform的position属性
            transform.position = curPosition;
        }
    }
}
