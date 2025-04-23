using System.Collections;
using UnityEngine;

public class FantasmaEnemy : MonoBehaviour
{
    public GameObject estilinguePrefab;
    public GameObject estilingueSpawnPoint;

    public PlayerInputSystem playerInputSystem;
    public float distanciaPlayer;

    public float attackCooldown = 2f;

    private float cooldownTimer;

    void Start()
    {
        playerInputSystem = GameObject.Find("Player").GetComponent<PlayerInputSystem>();
    }

    void Update()
    {
        distanciaPlayer = Vector2.Distance(transform.position, playerInputSystem.transform.position);

        if (distanciaPlayer < 8f)
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }

            //Olhando para o player.
            Vector2 lookDirection = playerInputSystem.transform.position - transform.position;
            transform.right = lookDirection.normalized;

            Vector3 localScale = transform.localScale;
            localScale.y = (playerInputSystem.transform.position.x < transform.position.x) ? -Mathf.Abs(localScale.y) : Mathf.Abs(localScale.y);
            transform.localScale = localScale;

            if (cooldownTimer <= 0f)
            {
                AttackPlayer();
                cooldownTimer = attackCooldown;
            }
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
        GameObject estilingue = Instantiate(estilinguePrefab, estilingueSpawnPoint.transform.position, Quaternion.identity);
        Vector3 scale = estilingue.transform.localScale;
        scale.y = transform.localScale.y;
        estilingue.transform.localScale = scale;
        //Define a direção do estilingue para o player.
        Vector2 direction = (playerInputSystem.transform.position - estilingueSpawnPoint.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        estilingue.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.3f);

        estilingue.GetComponent<FantasmaEstilingue>().Atirar(direction);
    }
}
