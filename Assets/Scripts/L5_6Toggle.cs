using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class L5_6Toggle : MonoBehaviour
{
    private Toggle toggle;
    public ToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(onValueChange);
    }

    private void onValueChange(bool bl)
    {
        Debug.Log("onValueChange" + bl);
    }

    #region ���ֻ�ȡѡ��Toggle�ķ�ʽ�������ã����ǹ��ڵڶ����������ƺ�����ʱ�������ã����Ͽ�����ʹ��ʱ����˵���У����������û����(toggle group�޷�ֱ����ק��������°汾��Ҫ�ڸ��������� Toggle Group�����)
    /// <summary>
    /// ��ȡ��ѡ�е�Toggle
    /// </summary>
    /// <returns></returns>
    public Toggle GetSelectedToggle()
    {
        Toggle toggle = toggleGroup.GetComponentsInChildren<Toggle>().Where(t => t.isOn).FirstOrDefault();

        return toggle;
    }

    /// <summary>
    /// ��ȡ��ѡ�е�Toggle
    /// </summary>
    /// <returns></returns>
    public Toggle GetSelectedToggleNew()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        return toggle;
    }
    #endregion
}
