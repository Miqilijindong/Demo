using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.对象池
{
    public class Manager : MonoBehaviour
    {
        public static Manager _instance;
        private ObjectPool<Stuff> objectPool;
        public GameObject objectPrefab;
        public List<Stuff> inGameObject;

        [HideInInspector]
        public float MaxX, MaxY;

        private void Awake()
        {
            _instance = this;
            objectPool = new ObjectPool<Stuff>(InstanceObject, 4);
            objectPrefab = Resources.Load<GameObject>("Prefabs/Object");
        }

        private void Start()
        {
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
            MaxX = vector3.x;
            MaxY = vector3.y;
        }

        Vector3 mousePos;
        private void Update()
        {
            for (int i = 0; i < inGameObject.Count; i++)
            {
                if (inGameObject[i].IsOutOfBoundary())
                {
                    Recall(inGameObject[i]);
                    inGameObject.RemoveAt(i);
                }
            }

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Stuff stuff = objectPool.GetObject();
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = transform.position.z;
                stuff.Init(mousePos);
            }
        }

        /// <summary>
        /// 实例化球的预制体，并添加脚本
        /// </summary>
        /// <returns></returns>
        private Stuff InstanceObject()
        {
            GameObject ball = Instantiate(objectPrefab, transform);
            ball.SetActive(false);
            Stuff stuff = ball.AddComponent<Stuff>();
            return stuff;
        }

        /// <summary>
        /// 回收Object
        /// </summary>
        /// <param name="stuff"></param>
        private void Recall(Stuff stuff)
        {
            stuff.gameObject.SetActive(false);
            objectPool.AddObject(stuff);
        }
    }
}
