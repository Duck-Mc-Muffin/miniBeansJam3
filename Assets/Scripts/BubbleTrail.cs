using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrail : MonoBehaviour
{
    public ParticleSystem bubbleTrail;
    public float bubbleTrailVel;
    private Rigidbody phy;

    private void Awake()
    {
        phy = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        if (phy.velocity.magnitude > bubbleTrailVel && !bubbleTrail.isPlaying) bubbleTrail.Play();
        else if (phy.velocity.magnitude < bubbleTrailVel && bubbleTrail.isPlaying) bubbleTrail.Stop();
    }
}
