using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMilitar : MonoBehaviour
{
    public float vidaBase;

    GameManager gameController;
    public Slider Vida;

    void Start()
    {
        gameController = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        Vida.value = vidaBase;

        if (vidaBase <= 0)
        {
            gameController.Win();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            RecibirDaño();
            Destroy(collision.gameObject);
        }
    }

    private void RecibirDaño()
    {
        vidaBase -= 25;

        Vida.value = vidaBase;
    }
}
