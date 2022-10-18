using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6_6物理射线 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 向上的射线
        //Ray ray = new Ray(Vector3.zero, new Vector3(0, 1,0));

        // 根据鼠标指向绘制射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if(Physics.Raycast(ray))
        //{
        //    Debug.Log("碰到东西了");
        //}
        // 射线延长1000---raycastHit射线碰撞物体的信息
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000))
        {
            // 射线碰到了什么物体
            Debug.LogFormat("射线碰撞到{0}, 位置:{1}", raycastHit.collider.name, raycastHit.point);

            // 这个是绘制模拟射线，玩家是看不到的
            Debug.DrawLine(ray.origin, raycastHit.point);
        }
    }
}
