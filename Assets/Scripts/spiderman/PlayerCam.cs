using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ����������
/// </summary>
public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    /// <summary>
    /// Ŀ�굥λ
    /// �������и�����Ȼ��Scene��ͼ�е�Toggle Tool Handle Position���ó���Center(����)�Ļ����ͻ������λ�ƹ��߸��ƶ������弰����������������ĵ���
    /// �����Pivot(���ĵ�)�Ļ�����ֻ�ǵ�ǰ��������ĵ��ϡ�
    /// </summary>
    public Transform Orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        

        //None�������� �������
        //Locked�����������ᱻ��������Ļ�����ĵ㣨���һᱻ���أ�
        //Confined������������ڴ��ڷ�Χ��
        Cursor.lockState = CursorLockMode.Locked;
        //true����ʾ
        //false������
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
        // �޸����������Ұ 
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTile(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
