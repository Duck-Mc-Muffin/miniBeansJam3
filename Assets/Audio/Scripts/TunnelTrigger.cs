using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance tunnelSnapshot;


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Player" || collider.gameObject.name == "Player_v2")
        {
          tunnelSnapshot = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.TunnelSnapshot);
            tunnelSnapshot.start();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.name == "Player_v2")
        {
            tunnelSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

}
