using UnityEngine;

/// <summary>
/// IOS����������
/// </summary>
public class SafeAreaFitter : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    public bool drag;
    /// <summary>
    /// ƫ����
    /// </summary>
    public float offset;
    void Start()
    {
        Rect safeArea = Screen.safeArea;
        float height = Screen.height - safeArea.height; //  ��ȡ�����߶�
        //Rect safeArea = Screen.safeArea;
        //float height = Screen.height - safeArea.height; //  ��ȡ�����߶�
#if UNITY_EDITOR
        Debug.Log("====== " + height);
#endif
        if (height > 0 && !drag)    //����λ��
        {
            float h = height / 2 + offset;
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            Vector2 pos = rectTransform.anchoredPosition;
            pos = new Vector2(pos.x, pos.y - (h));
            rectTransform.anchoredPosition = pos;
        }
        else if (height > 0 && drag)    //��������
        {
            float h = height / 2 + offset;
            RectTransform rectTransform = this.GetComponent<RectTransform>();

            Vector2 size = rectTransform.sizeDelta;
            size.y = h;
            rectTransform.sizeDelta = size;
        }
    }
}
