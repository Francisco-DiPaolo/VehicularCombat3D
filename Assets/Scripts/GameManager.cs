using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    public GameObject boss;
    public Transform spawnerPosition;
    public bool boolSpawnerBoss = false;
    public bool boolUltimateFaseBoss = false;

    private TankController player;

    [Header("Text")]
    public Text textContador;
    public Text textLife;

    private void Start()
    {
        player = FindObjectOfType<TankController>();
        textContador.text = "Score: " + score.ToString();
        textLife.text = "Vida: " + player.vidaPlayer.ToString();
        score = 0;
    }

    public void SetTextLife()
    {
        textLife.text = "Vida: " + player.vidaPlayer.ToString();
    }

    public void Win()
    {
        SceneManager.LoadScene("Winner");
    }

    public void Lose()
    {
        SceneManager.LoadScene("GameOver");
    }
}
