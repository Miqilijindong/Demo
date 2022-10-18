using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAndOverrideSprite : MonoBehaviour
{
    public string spriteName;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePicture()
    {
        image.overrideSprite =  Resources.Load<Sprite>(spriteName);
    }
}
