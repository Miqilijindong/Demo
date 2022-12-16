//#define IsShowMessage // 宏

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.特性
{
    internal class Obsolete : MonoBehaviour
    {
        [Obsolete("此方法已弃用， 请用NewTest()")]
        static void Test()
        {
            Debug.Log("Test()");
        }

        static void NewTest()
        {
            Debug.Log("NewTest()");
        }

        /// <summary>
        /// 当有#define IsShowMessage 的话，就可以调用这个函数
        /// </summary>
        /// <param name="message"></param>
        [Conditional("IsShowMessage")]
        static void ShowMessage(string message)
        {
            Debug.Log(message);
        }

        void Start() 
        {
            Test();

            ShowMessage("test1");

            Debug.Log("test2");

            ShowMessage("test3");

        }
    }
}
