using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioManager))]  
public class CustomInspectorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();    //This goes first

        AudioManager scriptReference = (AudioManager)target;   
        if (GUILayout.Button("Play Music"))   
        {
            scriptReference.PlayMusic();  
        }
        if (GUILayout.Button("Stop Music"))    
        {
            scriptReference.StopMusicImmediate();   
        }

        if (GUILayout.Button("Transition1"))
        {
            scriptReference.SetParameterInt(scriptReference.music, FMODPaths.TransitionParameter, 1);
        }
    }
}