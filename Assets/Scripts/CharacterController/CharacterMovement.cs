using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float forwardSpeed = 7f, horizontalRotationSpeed = 0.6f, verticalRotationSpeed = 0.45f;
    public bool invert_X, invert_Y;
    //public float zRotStabelizing, zRotStabelizingThreshold;

    //public Text ui_debug_text;
    private Rigidbody phy;

    private void Start()
    {
        phy = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Input
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float x = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2);
            float y = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2);

            phy.AddTorque((invert_X ? -transform.up : transform.up) * horizontalRotationSpeed * x, ForceMode.Force);
            phy.AddTorque((invert_Y ? transform.right : -transform.right) * verticalRotationSpeed * y, ForceMode.Force);
        }

        // Forward movement
        phy.AddForce(transform.forward * forwardSpeed, ForceMode.Force);

        // Stabelize Z-Axis
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

        // Stabelize Z-Axis (Physics)
        //float zTilt = (Vector3.Angle(Vector3.up, transform.right) - 90) / 90;
        //if (Mathf.Abs(zTilt) > zRotStabelizingThreshold) phy.AddTorque(transform.forward * zRotStabelizing * zTilt, ForceMode.Force);
    }
}