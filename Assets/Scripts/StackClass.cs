using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 堆栈
 *      先进后出的数据集合
 */
public class StackClass : MonoBehaviour
{
    Stack stack = new Stack();
    Stack<string> stackStr = new Stack<string>();
    // Start is called before the first frame update
    void Start()
    {
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);
        foreach(var i in stack)
        {
            Debug.Log(i);
        }

        object v = stack.Pop();
        Debug.Log("弹出堆栈的数据:" + v);

        // 查询堆栈里最后一个值
        object v1 = stack.Peek();
        Debug.Log("查询堆栈的倒数第一个值:" + v1);

        myStack myStack = new myStack();
        myStack.push(1);
        myStack.push(2);
        myStack.push(3);
        object v2 = myStack.peek();
        Debug.Log("MyStack.pop:" + v2);

    }

    class myStack
    {
        class StackData
        {
            public StackData nextItem;
            public object value;
            public StackData(StackData next, object value)
            {
                nextItem = next;
                this.value = value;
            }
        }
        StackData top;

        public void push(object value)
        {
            top = new StackData(top, value);
        }

        public object pop()
        {
            object returnValue = top.value;
            top = top.nextItem;
            return returnValue;
        }
        public object peek()
        {
            object returnValue = top.value;
            return returnValue;
        }

    }
}
