using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������һ��3D��plane
/// ������3D����Ҫ�޸Ĺ����ж�
/// shader�޸ĳ� Unlit/Texture��MeshRenderer.Lighting.CastShadows = off, ReceiveShadows = False
/// Ȼ��Sprite.WrapMade�ǵøĳ�Repeat
/// </summary>
public class BreakGroundScroll : MonoBehaviour
{
    /// <summary>
    /// �����ٶȣ���Χ��(-1,1)֮��
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
         * "_MainTex"����Ҫ������������Ҳ��ͨ��  mainTextureOffset ���Է���
         * "_BumpMap" is the normal map.
         * "_BumpMap"�Ƿ�����ͼ
         * "_Cube" is the reflection cubemap.
         * "_Cube"�Ƿ���cubemap.����������ͼ��
         */
        mat.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
