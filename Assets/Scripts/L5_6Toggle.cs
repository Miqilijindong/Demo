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

    #region 两种获取选中Toggle的方式都可以用，但是关于第二个方法，似乎会有时候不起作用，网上看别人使用时，就说不行，但是我这边没问题(toggle group无法直接拖拽父组件。新版本需要在父组件上添加 Toggle Group组件。)
    /// <summary>
    /// 获取被选中的Toggle
    /// </summary>
    /// <returns></returns>
    public Toggle GetSelectedToggle()
    {
        Toggle toggle = toggleGroup.GetComponentsInChildren<Toggle>().Where(t => t.isOn).FirstOrDefault();

        return toggle;
    }

    /// <summary>
    /// 获取被选中的Toggle
    /// </summary>
    /// <returns></returns>
    public Toggle GetSelectedToggleNew()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        return toggle;
    }
    #endregion
}
