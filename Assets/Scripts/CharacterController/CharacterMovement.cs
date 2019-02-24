using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;

    public float forwardSpeed = 7f, horizontalRotationSpeed = 0.6f, verticalRotationSpeed = 0.45f, headBounceBack = 2f;
    public LayerMask headBounceLayer;
    public bool invert_X, invert_Y;
    public float zRotStabelizing = 1f, zRotStabelizingThreshold = 0.05f;
    public float stunnKnockBack = 50f, stunnTime = 5f;
    
    private Rigidbody phy;
    private float headBounceBackRadius, headBounceDistance;
    private float currentStunnTime;
    private bool stunned;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        phy = GetComponent<Rigidbody>();
        CapsuleCollider tmp = GetComponent<CapsuleCollider>();
        headBounceBackRadius = tmp.radius * 0.7f;
        headBounceDistance = tmp.height / 2f - tmp.radius + 0.2f;
    }

    private void FixedUpdate()
    {
        // Stunn
        if (stunned)
        {
            currentStunnTime += Time.deltaTime;
            if (currentStunnTime > stunnTime) stunned = false;
            else return;
        }

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
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);

        // Stabelize Z-Axis (Physics)
        float zTilt = (Vector3.Angle(Vector3.up, transform.right) - 90) / 90;
        if (Mathf.Abs(zTilt) > zRotStabelizingThreshold) phy.AddTorque(transform.forward * zRotStabelizing * zTilt, ForceMode.Force);

        // Head bounce back
        Ray headBounceRay = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(headBounceRay, headBounceBackRadius, headBounceDistance, headBounceLayer))
        {
            phy.AddForce(-transform.forward * headBounceBack, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Enemy":
                FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FishImpactSound, this.gameObject);
                Stunn((transform.position - collision.transform.position).normalized);
                break;

            case "Glas":
                FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.GlassImpactSound, this.gameObject);
                FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FishImpactSound, this.gameObject);
                break;
        }
    }

    private void Stunn(Vector3 dir)
    {
        stunned = true;
        currentStunnTime = 0f;
        phy.velocity = Vector3.zero;
        phy.AddForce(dir * stunnKnockBack, ForceMode.Impulse);
        phy.AddTorque(Vector3.up * Random.Range(-5f, 5f), ForceMode.Impulse);
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.DashSound, this.gameObject);
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FishImpactSound, this.gameObject);
    }
}