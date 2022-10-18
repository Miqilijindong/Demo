using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      
 *      引用类型    string int[] class interface---引用类型是存在堆上的
 *      值类型     byte (u)short (u)int (u)long bool enum struct---值类型是存储在栈上的,当然是也跟变量的声明有关系的，如果是成员变量的话，就是存储在堆上
 *      
 *      short   -32768到32767
 *      int     -2147383648到214748647
 *      folat   7位小数
 *      double  15-16位
 *      
 * is
 *      可检测值类型和引用类型，成功返回true；否则返回false
 * as
 *      as首先会判断 源数据类型是否是目标数据类型，不是的化，编译直接报错
 *      as装换成功，返回源数据类型存储的数据，否则返回null
 *      
 * 强制类型转换(显式类型转换)
 *      (1)高精度->低精度 需要强制类型转换
 *      注意：高精度数据类型转换为低精度类型时，若要转换成功需要共精度数据类的位数<=低精度数据类型时
 * 自动类型转换(隐式类型转换)
 *      (1)低精度->高精度 系统自动转换
 */

public class class33
{

}

public class class55 : class33
{

}
public class TypeChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = 10;
        string str = "Hello World";
        bool v = a is string;
        Debug.Log(v);

        class55 test = new class55();
        class33 newtest = test as class33;
        bool check = test is class33;

        int[] b = { 1, 2, 3 };
        //int[] c = a as int[];
        int[] c = b as int[];
        if (c != null)
        {
            Debug.Log(c.Length);
        }

        Debug.Log(check);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
