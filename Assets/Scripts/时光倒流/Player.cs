using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Player : TimeControlled
{
    float moveSpeed = 5;
    float jumpVelocity = 10;


    public override void TimeUpdate()
    {
        base.TimeUpdate();

        Vector2 pos = transform.position;

        pos.y += velocity.y * Time.deltaTime;
        velocity.y -= TimeController.gravity * Time.deltaTime;

        // 测试代码，限制物体掉落到-1
        if (pos.y < -1)
        {
            pos.y = -1;
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
