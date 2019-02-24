using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fischfutter : MonoBehaviour
{

    public Vector2 forceStrengthRange;
    public Vector2 downStrengthRange;
    public Vector2 DragRange;

    private float randomForceStrength;
    private float randomDownStrength;


    // Start is called before the first frame update
    private void Start()
    {
        randomForceStrength = Random.Range(forceStrengthRange.x, forceStrengthRange.y);
        randomDownStrength = Random.Range(downStrengthRange.x, downStrengthRange.y);

        GetComponent<Rigidbody>().drag = Random.Range(DragRange.x, DragRange.y);
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -randomDownStrength, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 lookAtPos = new Vector3(transform.parent.position.x, transform.position.y, transform.parent.position.z);
        Debug.Log("LookAt: " + lookAtPos);
        transform.LookAt(lookAtPos);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(randomForceStrength, 0, 0), ForceMode.Force);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, randomForceStrength), ForceMode.Force);
    }
}
