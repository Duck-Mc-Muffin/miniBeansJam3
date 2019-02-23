using UnityEngine;

public class AttackFish : MonoBehaviour
{
    public GameObject target;
    public bool followMode = false;

    private RandomMovement movementComponent;
    
    void Start()
    {
        movementComponent = GetComponent<RandomMovement>();
    }

    void Update()
    {
        movementComponent.enabled = !followMode;
    }
    
    void FixedUpdate()
    {
        if (followMode)
        {
            GetComponent<Rigidbody>().AddForce(target.transform.position - transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            followMode = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            followMode = false;
        }
    }
}
