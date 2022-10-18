using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveCamera : MonoBehaviour
{
    public float x, y;

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;
            //transform.rotation += new Quaternion(y, x, 0, 0);
        }
    }
}
