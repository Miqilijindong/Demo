using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// 状态进入
    /// </summary>
    void OnEnter();

    /// <summary>
    /// 持续Update
    /// </summary>
    void OnUpdate();

    /// <summary>
    /// 当切换状态
    /// </summary>
    void OnExit();
}
