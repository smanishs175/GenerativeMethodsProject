using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    [Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
    public GameObject player;
    public int score = 0;
    public GameObject mainCanvas;
    public TextMeshProUGUI mainScoreDisplay;
    private void Start()
    {
        if (gm == null)
            gm = gameObject.GetComponent<GameManager>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }
    public void Collect(int amount)
    {
        score += amount;
        mainScoreDisplay.text = "Score : "+score.ToString();
    }
}
