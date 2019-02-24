using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    public FMOD.Studio.EventInstance music;

    [Header("Lautstärke")]
    [Range(0.0f, 1.0f)]
    public float musicVolume = 1f;
    [Range(0.0f, 1.0f)]
    public float sfxVolume = 1f;

    #region Unity Stuff

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //if more scenes
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void Start()
    {
        PlayMusic();
    }

    void Update()
    {
        FMODUnity.RuntimeManager.GetVCA(FMODPaths.VCA_MUSIC).setVolume(musicVolume); // 0 - 1f
        FMODUnity.RuntimeManager.GetVCA(FMODPaths.VCA_SFX).setVolume(sfxVolume); // 0 - 1f

        
    }


    #endregion


    #region Music stuff

    public void PlayMusic()
        {
            FMOD.Studio.PLAYBACK_STATE _music;
            music.getPlaybackState(out _music);
            if (_music != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                music = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.MusicEvent);
                music.start();
            }
        }

    public void StopMusicFade()
    {
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopMusicImmediate()
    {
        FMOD.Studio.Bus musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SetParameterInt(FMOD.Studio.EventInstance instance, string parameter, int value)
    {
        FMOD.Studio.ParameterInstance _parameter;
        instance.getParameter(parameter, out _parameter);

        _parameter.setValue(value);
    }

    public void SetParameterFloat(FMOD.Studio.EventInstance instance, string parameter, float value)
    {
        FMOD.Studio.ParameterInstance _parameter;
        instance.getParameter(parameter, out _parameter);

        _parameter.setValue(value);
    }

    #endregion


}

