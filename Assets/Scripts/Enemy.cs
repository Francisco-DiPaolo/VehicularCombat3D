using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float vidaEnemie = 100;
    public Transform destination;
    public Transform destino;
    public Transform origen;
    public Slider Vida;

    GameManager gameController;
    TankController playerMovement;
    SpriteRenderer spriteRenderer;

    NavMeshAgent navMeshAgent;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    private bool canShot;

    private Transform player;
    public Transform topTank;
    float distancia;
    [SerializeField] float distanciaShot;

    [SerializeField] Transform mira;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        gameController = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<TankController>();
        spriteRenderer = FindObjectOfType<SpriteRenderer>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        Vida.value = vidaEnemie;

        canShot = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Destruir();

        InvokeRepeating(nameof(SetDestination), 0f, 0.5f);

        Shooting();

        distancia = Vector3.Distance(transform.position, player.position);

        topTank.LookAt(player);
        mira.LookAt(player);
    }

    private void Destruir()
    {
        if (vidaEnemie <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shooting ()
    {
        if (distancia <= distanciaShot) 
        {
            Shooting(bulletPrefab, bulletForce);
        }
    }

    void Shooting(GameObject bulletPrefab, float bulletForce)
    {
        if (!canShot) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();

        rbBullet.AddForce(firePoint.forward * bulletForce, ForceMode.VelocityChange);

        StartCoroutine(ShotCD(3f));

        Destroy(bullet, 20);
    }

    private void RecibirDaño()
    {
        vidaEnemie -= 50;

        Vida.value = vidaEnemie;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            RecibirDaño();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destino"))
        {
            destination = origen;
        }

        if (other.gameObject.CompareTag("Origen"))
        {
            destination = destino;
        }
    }

    private void SetDestination()
    {
        navMeshAgent.SetDestination(destination.position);
    }

    public IEnumerator ShotCD(float delay)
    {
        canShot = false;
        yield return new WaitForSeconds(delay);
        canShot = true;
    }
}