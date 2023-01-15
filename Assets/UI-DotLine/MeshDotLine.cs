using System.Collections.Generic;
using UnityEngine;

public class MeshDotLine : MonoBehaviour
{
    private void Start()
    {
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null)
        {
            mf = gameObject.AddComponent<MeshFilter>();
        }
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
        {
            mr = gameObject.AddComponent<MeshRenderer>();
        }
        Mesh mesh = new Mesh();
        mf.mesh = mesh;
        mr.material = new Material(Shader.Find("Unlit/Color"));
        mr.material.SetColor("_Color", Color.yellow);
        CreateDotLineMesh(mesh, 100, 0.5f, 20);
    }

    /// <summary>
    /// 生成虚线网格.
    /// </summary>
    /// <param name="mesh">网格</param>
    /// <param name="length">总长度</param>
    /// <param name="cnt">虚线段数</param>
    private void CreateDotLineMesh(Mesh mesh, float length, float ratio, int cnt)
    {
        List<Vector3> vertices = new List<Vector3>();

        List<int> indices = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        float deltaLength = length / cnt;
        float solidLength = deltaLength * ratio;
        float deltaUv = 1.0f / cnt;
        float solidUv = deltaUv * ratio;
        for (int i = 0; i < cnt; i++)
        {
            float start = i * deltaLength;
            vertices.Add(new Vector3(start, 0, 0));
            vertices.Add(new Vector3(start + solidLength, 0, 0));
            indices.Add(2*i);
            indices.Add(2*i+1);

            float startUv = i * deltaUv;
            uvs.Add(new Vector2(startUv, 0));
            uvs.Add(new Vector2(startUv + solidUv, 0));
        }
        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
    }
}
