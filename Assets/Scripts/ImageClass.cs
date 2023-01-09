using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageClass : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        // 平滑增减---适用于进度条
        image.type = Image.Type.Filled;
        image.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
