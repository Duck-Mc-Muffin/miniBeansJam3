using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogik : MonoBehaviour
{
    public float maxTimeToLive;
    public string foodTag = "Food";
    public float foodTimeBonus = 10f;
    public ParticleSystem bubbleBurst, bubbleTrail;

    public Text txt_TTL;
    public Slider sld_TTL;

    public float bubbleTrailVel = 10f, test;

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

    private void Start()
    {
        TimeToLive = maxTimeToLive;
        phy = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TimeToLive -= Time.deltaTime;

        test = phy.velocity.magnitude;
        if (phy.velocity.magnitude > bubbleTrailVel && !bubbleTrail.isPlaying) bubbleTrail.Play();
        else if (phy.velocity.magnitude < bubbleTrailVel && bubbleTrail.isPlaying) bubbleTrail.Stop();
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
