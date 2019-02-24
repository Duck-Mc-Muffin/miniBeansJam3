using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool lockHorizontal = false;

    void LateUpdate()
    {
        if (Camera.main == null)
        {
            return;
        }

        var target = Camera.main.transform.position;
        if (lockHorizontal)
        {
            target.y = transform.position.y;
        }
        transform.LookAt(target);
    }
}
