using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 队列
 *      先进先出
 */
public class QueueClass : MonoBehaviour
{
    public Queue queue = new Queue();
    // Start is called before the first frame update
    void Start()
    {
        // 插入元素
        queue.Enqueue(0);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);

        // 出元素
        object v = queue.Dequeue();
        Debug.Log(v);

        foreach(var v1 in queue)
        {
            Debug.Log(v1);
        }


        MyQueue myQueue = new MyQueue();
        myQueue.Enqueue(1);
        myQueue.Enqueue(2);
        myQueue.Enqueue(3);
        myQueue.Enqueue(4);
        myQueue.Enqueue(5);

        Debug.Log(myQueue.Dequeue());
        Debug.Log(myQueue.Dequeue());
        Debug.Log(myQueue.Dequeue());
        Debug.Log(myQueue.Dequeue());
        Debug.Log(myQueue.Dequeue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class MyQueue
    {
        public class QueueData
        {
            public QueueData nextItem;
            public object value;

            public QueueData(QueueData lastItem, object value)
            {
                lastItem.nextItem = this;
                this.value = value;
            }
            public QueueData(object value)
            {
                this.value = value;
            }
        }
        public QueueData top;
        public QueueData last;

        public void Enqueue(object value)
        {
            if(top == null)
            {
                top = new QueueData(value);
                last = top;
            } else
            {
                last = new QueueData(last, value);
            }
        }

        public object Dequeue()
        {
            object value = top.value;
            top = top.nextItem;

            return value;
        }
    }
}
