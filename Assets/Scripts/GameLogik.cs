using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogik : MonoBehaviour
{
    public float maxTimeToLive;
    public string foodTag = "Food";
    public float foodTimeBonus = 10f;
    //public ParticleSystem particles;

    private float timeToLive;

    public Text txt_TTL;
    public Slider sld_TTL;

    public float TimeToLive
    {
        get { return timeToLive; }
        set
        {
            timeToLive = value;
            if (timeToLive <= 0) print("Player died!");
            txt_TTL.text = timeToLive > maxTimeToLive ? maxTimeToLive.ToString() : timeToLive.ToString("N2");
            txt_TTL.text += " Sec.";
            sld_TTL.value = timeToLive > maxTimeToLive ? 1 : timeToLive / maxTimeToLive;
        }
    }

    private void Start()
    {
        //particles = GetComponentInChildren<ParticleSystem>();
        TimeToLive = maxTimeToLive;
    }

    private void Update()
    {
        TimeToLive -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.tag);
        if (collision.transform.tag == foodTag)
        {
            if (TimeToLive > maxTimeToLive) return;
            TimeToLive += foodTimeBonus;
            Destroy(collision.gameObject);
            //particles.transform.position = collision.contacts[0].point;
            //particles.Play();
        }
    }
}
