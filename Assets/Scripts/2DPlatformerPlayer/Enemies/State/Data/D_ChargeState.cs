using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeData", menuName = "Data/State Data/Charge State")]
public class D_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6f;

    /// <summary>
    /// ≥Â∑Ê ±º‰
    /// </summary>
    public float chargeTime = 2f;
}
