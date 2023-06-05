using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    /*// ͨ��ʵ�ֵ���GenericNotImplementedError.TryGet();��ͳһCoreCompent����Ļ�ȡ����
    //������Ϊ���д����Ϊ�˹淶�����õģ�����
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
        private set => movement = value;
    }
    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
        private set => collisionSenses = value;
    }
    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
        private set => combat = value;
    }
    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(stats, transform.parent.name);
        private set => stats = value;
    }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Combat combat;
    private Stats stats;*/

    private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();

    private void Awake()
    {
        //Movement = GetComponentInChildren<Movement>();
        //CollisionSenses = GetComponentInChildren<CollisionSenses>();
        //combat = GetComponentInChildren<Combat>();
        //stats = GetComponentInChildren<Stats>();

    }

    public void LogicUpdate()
    {
        //Movement.LogicUpdate();
        //combat.LogicUpdate();

        foreach (CoreComponent component in coreComponents)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!coreComponents.Contains(component))
        {
            coreComponents.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = coreComponents.OfType<T>().FirstOrDefault();

        if (comp == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }
        return comp;
    }

    /// <summary>
    /// ���õ��ô���value����ֵGetComponent���ֵ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetComponentInChildren<T>();

        return value;
    }
}
