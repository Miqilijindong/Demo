using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// ״̬����
    /// </summary>
    void OnEnter();

    /// <summary>
    /// ����Update
    /// </summary>
    void OnUpdate();

    /// <summary>
    /// ���л�״̬
    /// </summary>
    void OnExit();
}
