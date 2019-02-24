using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSoundTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.PlantImpactSound, this.gameObject);
        }
    }
}
