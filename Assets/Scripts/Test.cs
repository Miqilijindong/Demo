using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody rigidbody;
    public int a;
    public int b;
    private int c;
    private int d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody != null && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * 12f, ForceMode.Impulse);
        }
    }
}
