using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class L9案例 : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 当点击右键时
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit raycast, 1000))
            {
                Debug.DrawRay(ray.origin, raycast.point);

                navMeshAgent.SetDestination(raycast.point);
                navMeshAgent.isStopped = false;
                animator.SetBool("run", true); 

            }
        }
        // 计算两点之间的距离(vector3 - vector3).magnitude
        if ((navMeshAgent.destination - transform.position).magnitude <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("run", false);
            navMeshAgent.isStopped = true;
        }
    }
}
