using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
    }
}
