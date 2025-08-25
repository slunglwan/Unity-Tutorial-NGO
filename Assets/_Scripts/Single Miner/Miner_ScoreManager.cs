using TMPro;
using UnityEngine;

public class Miner_ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] private TextMeshProUGUI scoreUI;

    private void Start()
    {
        scoreUI.text = $"Mineral : {score}";
    }

    public void AddScore()
    {
        score++;
        scoreUI.text = $"Mineral : {score}";
    }
}
