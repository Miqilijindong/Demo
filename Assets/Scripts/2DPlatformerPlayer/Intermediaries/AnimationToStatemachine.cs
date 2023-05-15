using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttact()
    {
        attackState.TriggerAttact();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
