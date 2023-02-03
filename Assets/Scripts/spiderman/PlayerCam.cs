using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
