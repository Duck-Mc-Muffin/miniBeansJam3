using System;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMovement : MonoBehaviour
{
    public float speed = 10f;

    [Tooltip("How often a new path is chosen")]
    public float pathInterval = 5f;

    private float pathTimer;
    private float perlinSeed;

    void Start()
    {
        pathTimer = Time.time + pathInterval;
        perlinSeed = Random.value * 10;
    }

    void Update()
    {
        if (pathTimer < Time.time)
        {
            perlinSeed = Random.value * 10;
            pathTimer = Time.time + pathInterval;
        }
    }
    
    void FixedUpdate()
    {
        var positionMatrix = new[]
        {
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back,
            Vector3.up,
            Vector3.down,
        };
        var minPerlin = positionMatrix.Aggregate(Vector3.positiveInfinity, (min, next) => LocalPerlinNoise(min) < LocalPerlinNoise(next) ? min : next);
        var force = minPerlin;
        force.Scale(new Vector3(speed, speed, speed));
        GetComponent<Rigidbody>().AddForce(force);
    }

    private float LocalPerlinNoise(Vector3 vector3)
    {
        vector3 += transform.position;
        vector3 += new Vector3(perlinSeed, perlinSeed, perlinSeed);
        return Perlin.Noise(vector3);
    }
}
