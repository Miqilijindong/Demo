using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L8_7角色控制 : MonoBehaviour
{
    public CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 vector3 = new Vector3(h, 0, v);

        // 是按照帧为单位的，且是不具有重力的
        characterController.Move(vector3 * Time.deltaTime);

        // 是按照秒为单位的，具有重力的
        //characterController.SimpleMove(vector3);

    }
}
