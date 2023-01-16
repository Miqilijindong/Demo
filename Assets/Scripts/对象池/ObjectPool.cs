using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T>
{
    private List<T> poolList = new List<T>();
    private Func<T> func;// 委托

    /// <summary>
    /// 对象池实例化
    /// </summary>
    /// <param name="func"></param>
    /// <param name="count"></param>
    public ObjectPool(Func<T> func, int count)
    {
        this.func = func;
        InstanceObject(count);
    }

    /// <summary>
    /// 实例化对象
    /// </summary>
    /// <param name="count"></param>
    private void InstanceObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolList.Add(func());
        }
    }

    /// <summary>
    /// 获取对象池里的对象
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        int i = poolList.Count;
        while (i-- > 0)
        {
            T t = poolList[i];
            poolList.RemoveAt(i);
            return t;
        }
        // 这里是为了防止对象池空了，此时再给它2个对象然后调用自身。
        InstanceObject(2);
        return GetObject();
    }

    /// <summary>
    /// 对象池里添加对象，回收对象
    /// </summary>
    /// <param name="t"></param>
    public void AddObject(T t)
    {
        poolList.Add(t);
    }
}
