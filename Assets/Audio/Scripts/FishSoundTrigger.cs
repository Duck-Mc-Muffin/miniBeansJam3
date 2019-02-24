using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSoundTrigger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {
          FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FishImpactSound, this.gameObject);      
        }
    }
}
