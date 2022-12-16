using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirePoint : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bulletPrefab;
    public Transform targetPos;
    public float bulletSpeed;

    float rotationAngle = 5f;
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//屏幕坐标转换世界坐标
            //var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
            worldPos.z = 1;
            FireFromFirePoint(worldPos);
        }
    }

    public void Fire()
    {
        GameObject go = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.bulletSpeed = bulletSpeed;
        bullet.targetPos = targetPos;
    }

    public void FireFromFirePoint(Vector3 firePos)
    {
        Vector3 vector3 = firePos - firePoint.transform.position;

        float angle = 90 - Mathf.Atan2(vector3.x, vector3.y) * Mathf.Rad2Deg;//tan值为x/z的角的弧度，转换为度数。
        //float angle_1 = 90 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;


            GameObject go = Instantiate(bulletPrefab, firePos, Quaternion.identity);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.bulletSpeed = bulletSpeed;
            bullet.targetPos = targetPos;
            bullet.rotationAngle = angle;
    }
}
