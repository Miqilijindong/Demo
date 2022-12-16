using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform targetPos;
    public float bulletSpeed;


    /// <summary>
    /// 子弹拐弯角度
    /// </summary>
    //[HideInInspector]
    public float rotationAngle;
    float MaxRotationAngle = 30;
    float MinRotationAngle = -30;

    private void Start()
    {
        // 转动角度---类似霰弹枪一样
        //ShotGun();

        //rotationAngle = Random.Range(MinRotationAngle, MaxRotationAngle);
    }
    private void Update()
    {
        FollowBullet3();
    }

    /// <summary>
    /// 追踪弹
    /// </summary>
    private void FollowBullet()
    {
        // 始终让它朝着目标
        Vector3 direction = targetPos.position - this.transform.position;
        // 方向向量转换为角度值
        float angle_1 = 90 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        // 将当前物体的角度设置为对应角度
        transform.eulerAngles = new Vector3(0, 0, angle_1);
        // 转动角度
        this.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationAngle);

        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 追踪弹2
    /// </summary>
    private void FollowBullet2()
    {
        // 始终让它朝着目标
        Vector3 direction = targetPos.position - this.transform.position;
        this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        // 转动角度
        this.transform.rotation *= Quaternion.Euler(0, 0, rotationAngle);


        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    public void FollowBullet3()
    {
        transform.up = Vector3.Slerp(transform.up, targetPos.position - transform.position, 0.5f / Vector2.Distance(this.transform.position, targetPos.position));
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 霰弹枪
    /// </summary>
    private void ShotGun()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
