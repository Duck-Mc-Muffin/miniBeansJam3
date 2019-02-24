using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundTrigger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Player_v2")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.GlassImpactSound, this.gameObject);
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FishImpactSound, this.gameObject);
        }
    }
}
