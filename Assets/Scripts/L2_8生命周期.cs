using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_8生命周期 : MonoBehaviour
{
    // 唤醒事件，一开始就执行，只执行一次。
    private void Awake()
    {
        Debug.Log("Awake");
    }
    // 启用事件，每一次启用都执行一次。当脚本组件被启用的时候执行一次
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    // 开始事件，执行一次    
    void Start()
    {
        Debug.Log("Start");       
    }
    // 固定更新事件，执行N次，0.02秒执行一次。所有物理相关的更新都在这个事件中处理。
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");        
    }
    // 更新时间，执行N次，没帧执行一次。
    void Update()
    {
        Debug.Log("Update");
    }
    // 稍后更新事件，执行N次，在update()事件执行完毕后再执行
    private void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
    // 禁用时间，每次禁用都执行一次。在onDestroy()事件也会执行。
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    // 销毁时间，执行一次。当组件被销毁时执行。
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
