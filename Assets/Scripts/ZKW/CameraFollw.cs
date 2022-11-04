using UnityEngine;

public class CameraFollw : MonoBehaviour
{

    // ��Ҫ�����Ŀ�����
    public Transform target;

    // ��Ҫ���������꣨����ʵʱ��Ч��
    public bool freazeX, freazeY, freazeZ;

    // �����ƽ��ʱ�䣨�������ͺ�ʱ�䣩
    public float smoothTime = 0.3F;
    private float xVelocity, yVelocity, zVelocity = 0.0F;

    // �����ƫ����
    private Vector3 offset;

    // ȫ�ֻ����λ�ñ���
    private Vector3 oldPosition;

    // ��¼��ʼλ��
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Debug.Log(1231);
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
            oldPosition.z = Mathf.SmoothDamp(transform.position.z, target.position.z + offset.z, ref zVelocity, smoothTime);
        }

        transform.position = oldPosition;
    }

    /// <summary>
    /// �������¿�ʼ��Ϸʱֱ���������λ��
    /// </summary>
    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}