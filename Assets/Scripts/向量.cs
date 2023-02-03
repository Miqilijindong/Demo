using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class 向量 : MonoBehaviour
{
    private void Start()
    {
        Vector3 vector3 = new Vector3(1, 1, 1);

        Debug.Log("取向量的长度:" + vector3.magnitude);
        // 向量方向的计算
        Debug.Log("取向量的方向量:" + vector3.normalized);

    }
}
