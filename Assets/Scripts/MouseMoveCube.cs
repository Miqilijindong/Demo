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
        //将物体由世界坐标系转换为屏幕坐标系
        Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
        //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //得到现在鼠标的2维坐标系位置       
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //curPosition就是物体应该的移动向量赋给transform的position属性
            transform.position = curPosition;
        }
    }
}
