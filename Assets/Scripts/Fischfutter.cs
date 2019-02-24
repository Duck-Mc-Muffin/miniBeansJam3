using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fischfutter : MonoBehaviour
{

    public Vector2 forceStrengthRange;
    public Vector2 downStrengthRange;
    public Vector2 DragRange;

    [HideInInspector]
    public float maxY;

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
        transform.LookAt(new Vector3(0, transform.position.y, 0), new Vector3(0, 1, 0));
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(randomForceStrength, 0, 0), ForceMode.Force);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, randomForceStrength), ForceMode.Force);
    }
}
