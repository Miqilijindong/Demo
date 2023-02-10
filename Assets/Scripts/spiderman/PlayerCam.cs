using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 玩家摄像机类
/// </summary>
public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    /// <summary>
    /// 目标单位
    /// 当物体有个子类然后Scene视图中的Toggle Tool Handle Position设置成了Center(居中)的话，就会把类似位移工具给移动到物体及其所有子物体的中心点上
    /// 如果是Pivot(中心点)的话，就只是当前物体的中心点上。
    /// </summary>
    public Transform Orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        

        //None：不锁定 解除锁定
        //Locked：锁定，鼠标会被锁定在屏幕的中心点（并且会被隐藏）
        //Confined：将鼠标限制在窗口范围内
        Cursor.lockState = CursorLockMode.Locked;
        //true：显示
        //false：隐藏
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        xRotation  = Mathf.Clamp(xRotation, -90, 90);

        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void DoFov(float endValue)
    {
        // 修改摄像机的视野 
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTile(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
