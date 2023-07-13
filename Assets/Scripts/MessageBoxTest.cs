
using UnityEngine;
using System.Windows;
using UnityEditor;

public class MessageBoxTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
        EditorUtility.DisplayDialog("title", "content", "确认", "取消");
    }
}
