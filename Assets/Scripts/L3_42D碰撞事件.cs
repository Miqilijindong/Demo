using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_42D碰撞事件 : MonoBehaviour
{
    /**
     * 1.双方都没有碰撞体和刚体，是绝对不可能触发碰撞事件(函数)
     * 2.双方都有碰撞体和刚体。
     * 3.一方有刚体和碰撞体，另外一方只有碰撞体(无论是哪一方，双方都可以进入碰撞事件)
     * 4.双方都没有刚体，无法进入
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("碰撞发生, 碰撞的名字是:" + collision.gameObject.name);
        Destroy(gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        print("碰撞结束");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        print("碰撞中");
    }
}
