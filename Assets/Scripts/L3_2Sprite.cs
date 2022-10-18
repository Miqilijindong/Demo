using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_2Sprite : MonoBehaviour
{
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.flipX = true;
        spriteRenderer.flipY = true;
        spriteRenderer.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
