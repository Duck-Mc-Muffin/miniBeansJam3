using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance tunnelSnapshot;


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Player")
        {
          tunnelSnapshot = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.TunnelSnapshot);
            tunnelSnapshot.start();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            tunnelSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

}
