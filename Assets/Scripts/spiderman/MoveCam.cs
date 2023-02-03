using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform camTransfrom;
    // Update is called once per frame
    void Update()
    {
        transform.position = camTransfrom.position;
    }
}
