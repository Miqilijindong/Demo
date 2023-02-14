using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

class RandomScript : MonoBehaviour
{
    private void Start()
    {
        float v = Random.Range(0f, 1f);// 是有可能返回0、1的
        Debug.Log(v);

        Debug.Log(Random.Range(0, 1));// 返回结果永远是0!
    }
}
