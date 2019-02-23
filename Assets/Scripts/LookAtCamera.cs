using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public bool lockHorizontal = false;

    void Update()
    {
        if (Camera.current == null)
        {
            return;
        }

        var target = Camera.current.transform.position;
        if (lockHorizontal)
        {
            target.y = transform.position.y;
        }
        transform.LookAt(target);
    }
}
