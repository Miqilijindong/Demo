using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : TimeControlled
{
    float moveSpeed = 5;
    float jumpVelocity = 10;


    public override void TimeUpdate()
    {
        Vector2 pos = transform.position;

        pos.y += velocity.y * Time.deltaTime;
        velocity.y -= TimeController.gravity * Time.deltaTime;

        if (pos.y < -4)
        {
            pos.y = -4;
            velocity.y = 0; 
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            velocity.y = jumpVelocity;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        transform.position = pos;   
    }


}
