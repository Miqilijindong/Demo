using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class L9_1导航系统 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform target;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        Debug.Log(target.position);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            navMeshAgent.isStopped = !navMeshAgent.isStopped;
            Debug.Log("修改是否跟随, isStopped:" + navMeshAgent.isStopped);
        }
    }
}
