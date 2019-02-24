using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogik : MonoBehaviour
{
    public static GameLogik instance; 

    public float maxTimeToLive, gameOverPause;
    public string foodTag = "Food";
    public float foodTimeBonus = 10f;
    public ParticleSystem bubbleBurst;

    public Text txt_Score;
    public Slider sld_TTL;
    public int sceneIndex_DeathScreen;

    private float timeToLive, currentScoreTime, currentGameOverTime;
    private Rigidbody phy;

    public float TimeToLive
    {
        get { return timeToLive; }
        set
        {
            timeToLive = value;
            if (timeToLive <= 0)
            {
                timeToLive = 0;
                CharacterMovement.instance.enabled = false;
                PlayerPrefs.SetString("LastTime", txt_Score.text);
                print(PlayerPrefs.GetString("LastTime"));
            }
            sld_TTL.value = timeToLive > maxTimeToLive ? 1 : timeToLive / maxTimeToLive;
            AudioManager.instance.SetParameterFloat(AudioManager.instance.music, FMODPaths.TimeParameter, TimeToLive);
        }
    }

    public float ScoreTime
    {
        get { return currentScoreTime; }
        set
        {
            currentScoreTime = value;
            int minutes = Mathf.FloorToInt(currentScoreTime) / 60;
            float seconds = (currentScoreTime - minutes * 60);
            txt_Score.text = minutes + ":" + (seconds < 10 ? "0" + seconds.ToString("N2") : seconds.ToString("N2")).Replace(',', '.'); // TODO: Formatierung ändern.
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TimeToLive = maxTimeToLive;
        phy = GetComponent<Rigidbody>();
        ScoreTime = 0;
    }

    private void Update()
    {
        if (TimeToLive > 0)
        {
            ScoreTime += Time.deltaTime;
            TimeToLive -= Time.deltaTime;
        }
        else
        {
            currentGameOverTime += Time.deltaTime;
            if (currentGameOverTime > gameOverPause) SceneManager.LoadScene(sceneIndex_DeathScreen);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == foodTag)
        {
            if (TimeToLive > maxTimeToLive || TimeToLive <= 0) return;
            TimeToLive += foodTimeBonus;
            Destroy(collision.gameObject);
            bubbleBurst.transform.position = collision.contacts[0].point;
            bubbleBurst.Play();
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.FoodBubbleSound, this.gameObject);
        }
    }
}
