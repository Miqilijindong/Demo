using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10_2实例化 : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 1000))
            {
                Debug.Log(raycastHit.point);
                GameObject.Instantiate(gameObject, raycastHit.point, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
