using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClass : MonoBehaviour
{
    public static int damage = 50;
    public List<enemyClass> enemies;
    public static playerClass instance;
    public int scores = 0;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void FixedUpdate()
    {

    }

    private void shoot()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000))
            {
                Debug.DrawRay(ray.origin, raycastHit.point, Color.blue);
                //Debug.Log(raycastHit.transform.tag);
                if (raycastHit.transform.tag == "enemy")
                {
                    enemyClass enemyClass = raycastHit.collider.gameObject.GetComponent<enemyClass>();
                    if (enemyClass == null)
                    {
                        enemyClass = raycastHit.collider.gameObject.GetComponentInParent<enemyClass>();
                    }
                    enemyClass.getDamage(damage);
                }
                if(enemies.Count <= 0)
                {
                    Win();
                }
            }
        }
    }

    public void Win()
    {
        UIManager.instance.showGameResult(true);
    }
    public void Lose()
    {
        Time.timeScale = 0;
        UIManager.instance.showGameResult(false);
    }
}
