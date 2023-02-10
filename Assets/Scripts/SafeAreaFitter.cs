using UnityEngine;

/// <summary>
/// IOS适配刘海屏
/// </summary>
public class SafeAreaFitter : MonoBehaviour
{
    /// <summary>
    /// 拉伸
    /// </summary>
    public bool drag;
    /// <summary>
    /// 偏移量
    /// </summary>
    public float offset;
    void Start()
    {
        Rect safeArea = Screen.safeArea;
        float height = Screen.height - safeArea.height; //  获取刘海高度
        //Rect safeArea = Screen.safeArea;
        //float height = Screen.height - safeArea.height; //  获取刘海高度
#if UNITY_EDITOR
        Debug.Log("====== " + height);
#endif
        if (height > 0 && !drag)    //朝下位移
        {
            float h = height / 2 + offset;
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 pos = rectTransform.anchoredPosition;
            pos = new Vector2(pos.x, pos.y - (h));
            rectTransform.anchoredPosition = pos;
        }
        else if (height > 0 && drag)    //朝下拉伸
        {
            float h = height / 2 + offset;
            RectTransform rectTransform = this.GetComponent<RectTransform>();

            Vector2 size = rectTransform.sizeDelta;
            size.y = h;
            rectTransform.sizeDelta = size;
        }
    }
}
