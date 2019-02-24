using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreGet : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        IEnumerable<HighScore.Score> scores = HighScore.GetHighscores();
        foreach (HighScore.Score item in scores)
        {
            GetComponent<Text>().text += string.Format("{0}. {1}: {2}", item.Position + 1, item.Username, item.Points);
        }
    }
    
}
