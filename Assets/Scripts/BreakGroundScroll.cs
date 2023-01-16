using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 首先生成一个3D的plane
/// 由于是3D物体要修改光线判断
/// shader修改成 Unlit/Texture，MeshRenderer.Lighting.CastShadows = off, ReceiveShadows = False
/// 然后Sprite.WrapMade记得改成Repeat
/// </summary>
public class BreakGroundScroll : MonoBehaviour
{
    /// <summary>
    /// 滚动速度，范围在(-1,1)之间
    /// </summary>
    [Range(-1, 1)]
    public float scrollSpeed = 0.5f;
    private float offSet;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offSet += (Time.deltaTime * scrollSpeed) / 10f;
        /**
         * "_MainTex" is the main diffuse texture .This can also be accessed via  mainTextureOffset property.
         * "_MainTex"是主要的漫反射纹理，也能通过  mainTextureOffset 属性访问
         * "_BumpMap" is the normal map.
         * "_BumpMap"是法线贴图
         * "_Cube" is the reflection cubemap.
         * "_Cube"是反射cubemap.（立方体贴图）
         */
        mat.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
