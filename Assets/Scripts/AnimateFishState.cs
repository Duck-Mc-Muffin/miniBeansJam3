using System.Linq;
using UnityEngine;

public class AnimateFishState : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    
    void Start()
    {
        animator.SetInteger("Direction", -1);
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        var playerLevel = player.transform.position.y - transform.position.y;
        animator.SetFloat("PlayerLevel", playerLevel);

        var localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        localVelocity.Normalize();

        if (Vector3.Distance(localVelocity, Vector3.left) < 0.5f)
        {
            animator.SetInteger("Direction", 3);
        } else if (Vector3.Distance(localVelocity, Vector3.right) < 0.5f)
        {
            animator.SetInteger("Direction", 1);
        }
        else
        {
            animator.SetInteger("Direction", -1);
        }
    }
}
