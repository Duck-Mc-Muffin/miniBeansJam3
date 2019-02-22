using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fischfutter : MonoBehaviour
{

    public float forceStrength;
    

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(new Vector3(0, transform.position.y, 0), new Vector3(0, 1, 0));
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(forceStrength, 0, 0), ForceMode.Force);
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, forceStrength), ForceMode.Force);
    }
}
