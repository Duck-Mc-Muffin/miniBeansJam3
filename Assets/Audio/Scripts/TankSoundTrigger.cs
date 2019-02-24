using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundTrigger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.GlassImpactSound, this.gameObject);
        }
    }
}
