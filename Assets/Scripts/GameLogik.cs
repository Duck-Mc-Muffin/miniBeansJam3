using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogik : MonoBehaviour
{
    public static GameLogik instance; 

    public float maxTimeToLive;
    public string foodTag = "Food";
    public float foodTimeBonus = 10f;
    public ParticleSystem bubbleBurst;

    public Text txt_TTL;
    public Slider sld_TTL;

    private float timeToLive;
    private Rigidbody phy;

    public float TimeToLive
    {
        get { return timeToLive; }
        set
        {
            timeToLive = value;
            if (timeToLive <= 0) print("Player died!");
            txt_TTL.text = timeToLive > maxTimeToLive ? maxTimeToLive.ToString() : timeToLive.ToString("N2").Replace(',', '.'); // TODO: Formatierung ändern.
            txt_TTL.text += " sec.";
            sld_TTL.value = timeToLive > maxTimeToLive ? 1 : timeToLive / maxTimeToLive;
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
    }

    private void Update()
    {
        TimeToLive -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == foodTag)
        {
            if (TimeToLive > maxTimeToLive) return;
            TimeToLive += foodTimeBonus;
            Destroy(collision.gameObject);
            bubbleBurst.transform.position = collision.contacts[0].point;
            bubbleBurst.Play();
        }
    }
}
