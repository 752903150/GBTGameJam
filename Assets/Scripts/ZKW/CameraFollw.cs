using UnityEngine;

public class CameraFollw : MonoBehaviour
{

    // 需要跟随的目标对象
    public Transform target;

    // 需要锁定的坐标（可以实时生效）
    public bool freazeX, freazeY, freazeZ;

    // 跟随的平滑时间（类似于滞后时间）
    public float smoothTime = 0.3F;
    private float xVelocity, yVelocity, zVelocity = 0.0F;

    // 跟随的偏移量
    private Vector3 offset;

    // 全局缓存的位置变量
    private Vector3 oldPosition;

    // 记录初始位置
    private Vector3 startPosition;

    void Start()
    {
        transform.position = target.position;
        offset = Vector3.zero;
    }

    void LateUpdate()
    {
        oldPosition = transform.position;

        if (!freazeX)
        {
            oldPosition.x = Mathf.SmoothDamp(transform.position.x, target.position.x + offset.x, ref xVelocity, smoothTime);
        }

        if (!freazeY)
        {
            oldPosition.y = Mathf.SmoothDamp(transform.position.y, target.position.y + offset.y, ref yVelocity, smoothTime);
        }

        if (!freazeZ)
        {
            oldPosition.z = -10f;
        }

        transform.position = oldPosition;
    }

    /// <summary>
    /// 用于重新开始游戏时直接重置相机位置
    /// </summary>
    public void ResetPosition()
    {
        target.position = target.position;
        
    }
}