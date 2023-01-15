using UnityEngine;

public class GeoDotLineDemo : MonoBehaviour
{
    void Start()
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
        mr.material = new Material(Shader.Find("Custom/Geometry/GeoDotLine"));

        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 100, 0),
        };
        mesh.SetIndices(new int[]{0, 1}, MeshTopology.LineStrip, 0);
        mesh.uv = new Vector2[]
        {
            Vector2.zero,
            new Vector2(1, 1),
        };
    }

}
