using System.Collections;
using UnityEngine;

public class FantasmaEnemy : MonoBehaviour
{
    public GameObject estilinguePrefab;
    public GameObject estilingueSpawnPoint;
    public Rigidbody2D rb;
    public Vector3 direction;

    public PlayerInputSystem playerInputSystem;
    public float distanciaPlayer;
    public Health fantasmaHealth;

    public float attackCooldown = 2f;
    public float speed = 3f;

    private float cooldownTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fantasmaHealth = GetComponent<Health>();
        playerInputSystem = GameObject.Find("Player").GetComponent<PlayerInputSystem>();

        direction = (playerInputSystem.transform.position - transform.position).normalized;
    }

    void Update()
    {
        distanciaPlayer = Vector2.Distance(transform.position, playerInputSystem.transform.position);

        if (fantasmaHealth.isDead)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.linearVelocity = Vector2.zero;
            Destroy(this.gameObject, 0.7f);
        }
        else if (distanciaPlayer <= 15f)
        {
            if (distanciaPlayer <= 6f)
            {
                if (cooldownTimer <= 0f)
                {
                    AttackPlayer();
                    cooldownTimer = attackCooldown;
                }
                else
                {
                    cooldownTimer -= Time.deltaTime;
                }

                //Olhando para o player.
                Vector2 lookDirection = playerInputSystem.transform.position - transform.position;
                transform.right = lookDirection.normalized;

                Vector3 localScale = transform.localScale;
                localScale.y = (playerInputSystem.transform.position.x < transform.position.x) ? -Mathf.Abs(localScale.y) : Mathf.Abs(localScale.y);
                transform.localScale = localScale;
            }

            if (distanciaPlayer <= 4f)
            {
                transform.position += new Vector3(direction.x, 0.2f, direction.z) * speed / 3 * Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerInputSystem.transform.position, speed * Time.deltaTime);
            }

            rb.MovePosition(transform.position);
        }
        else
        {
            Debug.Log("Player muito longe!!");
        }
    }

    public void AttackPlayer()
    {
        StartCoroutine(EstilingueBullet());

        Debug.Log("Fantasma atacou o jogador!");
    }

    IEnumerator EstilingueBullet()
    {
        //Instancia o estilingue na posição do ponto de spawn.
        GameObject estilingue = Instantiate(estilinguePrefab, estilingueSpawnPoint.transform.position, Quaternion.identity, this.transform);
        //Define a direção do estilingue para o player.
        Vector2 direction = (playerInputSystem.transform.position - estilingueSpawnPoint.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        estilingue.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.3f);

        estilingue.GetComponent<FantasmaEstilingue>().Atirar(direction);
    }
}
