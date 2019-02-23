using System.Linq;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Logic")]
    public GameObject fishPrefab;
    public int amount = 10;

    [Header("Editor Preview")] public bool drawGizmos = false;
    public Color gizmosColor = new Color(0f, 255f, 255f, 0.5f);

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var randomPos = RandomVector3(transform);
            var g = Instantiate(fishPrefab, randomPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Draw Gizmos for a nicer area preview.
    /// </summary>
    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }

    /// <summary>
    /// Will return a random <see cref="Vector3"/> within the given bounds.
    /// </summary>
    /// <param name="bounds">Area constraint</param>
    /// <returns>Random Vector3</returns>
    private Vector3 RandomVector3(Transform bounds)
    {
        Vector3 origin = bounds.position;
        Vector3 range = bounds.localScale / 2.0f;
        Vector3 randomRange = new Vector3(
            Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y),
            Random.Range(-range.z, range.z));
        return origin + randomRange;
    }
}
