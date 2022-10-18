using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClass : MonoBehaviour
{
    private static enemyClass instance;
    public int speed = 1;
    private Rigidbody rigidbody;
    public int healthy = 100;
    void Start()
    {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector3 vector3 = new Vector3(0, 0, speed);
        rigidbody.MovePosition(transform.position + vector3 * Time.deltaTime);
    }

    public void getDamage(int damage)
    {
        Debug.Log(transform.name + ":我受到了" + damage + "点伤害");
        healthy -= damage;
        if(healthy <= 0)
        {
            dead();
        }
    }

    public void dead()
    {
        playerClass.instance.scores++;
        UIManager.instance.ChangeScorse(playerClass.instance.scores);
        playerClass.instance.enemies.Remove(this);
        Destroy(gameObject);
    }
}
