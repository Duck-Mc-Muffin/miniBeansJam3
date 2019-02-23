using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody player;
    public float distance = 3f, knockBack = 3f;

    private Vector3 lastPoint;

    private void Start()
    {
        lastPoint = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, lastPoint) > distance)
        {
            transform.position = lastPoint;
            lastPoint = player.position;
        }
    }

    public void OnPlayerCollided()
    {
        player.AddForce((transform.position - player.position).normalized * knockBack, ForceMode.Impulse);
    }
}
