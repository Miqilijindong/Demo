using UnityEngine;

public class BGController : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer bg;

    private void Start()
    {
        bg = GetComponent<SpriteRenderer>();
        float width = bg.sprite.bounds.size.x;
        float Height = bg.sprite.bounds.size.y;
        float widthScale = CameraController_Scaler.cameraWidthSize / width;
        float heightScale = CameraController_Scaler.cameraHeightSize / Height;
        bg.transform.localScale = new Vector3(widthScale, heightScale, 1.0f);
    }
}