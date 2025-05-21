using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Pontos do patrulha")]
    public Animator anim;
    public Rigidbody2D rb;
    public Transform pontoDireita;
    public Transform pontoEsquerda;
    public PlayerInputSystem playerInputSystem;
    public CutSceneBoss cutSceneBoss;
    public bool IsAttacking = false;
    public GameObject projetilPrefab;
    public GameObject projetilInstance;
    public bool projetilAttack = false;
    public float forcaTiro = 10f;
    public Transform pontoDeTiro;


    [Header("Configurações do patrulha")]
    public float velocidade = 4f;
    public float tempoDeEspera = 1f;
    public float distanciaDeParada = 0.1f;
    public float AttackCooldown;
    public float AttackCooldownTarget;

    public Coroutine patrulhaCoroutine;
    public bool estaPatrulhando = false;
    public bool isRight = true;
    public Health Health;

    void Start()
    {
        anim = GetComponent<Animator>();
        Health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        playerInputSystem = FindFirstObjectByType<PlayerInputSystem>();
        cutSceneBoss = FindFirstObjectByType<CutSceneBoss>();
    }

    void Update()
    {
        anim.SetBool("Run", estaPatrulhando);
        anim.SetBool("Attack", IsAttacking);
        anim.SetBool("Death", Health.isDead);

        if (Health.isDead)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            if (isRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }


            if (cutSceneBoss.isFighting)
            {
                AttackCooldown += Time.deltaTime;
                if (!estaPatrulhando && !IsAttacking && patrulhaCoroutine == null)
                {
                    patrulhaCoroutine = StartCoroutine(Patrulha());
                }
            }

            if (AttackCooldown >= AttackCooldownTarget)
            {
                AttackCooldown = 0;
                // Aqui você pode adicionar a lógica de ataque do boss
                StopAllCoroutines();
                Debug.Log("Atacando o jogador!");
                StartCoroutine(Attack());
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    Debug.Log("Ataque finalizado");
                    StartCoroutine(Patrulha());
                    estaPatrulhando = true;
                }
            }

            if (projetilAttack)
            {
                if (projetilInstance == null)
                {
                    Vector2 direcao = playerInputSystem.transform.position - pontoDeTiro.position;
                    projetilInstance = Instantiate(projetilPrefab, pontoDeTiro.position, Quaternion.identity);
                    projetilInstance.GetComponent<Rigidbody2D>().linearVelocity = direcao.normalized * forcaTiro;

                    //O objeto rotaciona para a direção do tiro.
                    float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                    projetilInstance.transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

                    Destroy(projetilInstance, 2f);
                    projetilAttack = false;
                }
            }
        }

    }

    IEnumerator Attack()
    {
        IsAttacking = true;
        estaPatrulhando = false;

        if (patrulhaCoroutine != null)
        {
            StopCoroutine(patrulhaCoroutine);
            patrulhaCoroutine = null;
        }

        yield return new WaitForSeconds(0.4f);
        projetilAttack = true;

        yield return new WaitForSeconds(0.6f);

        Debug.Log("Ataque finalizado");
        IsAttacking = false;

        if (cutSceneBoss.isFighting)
        {
            patrulhaCoroutine = StartCoroutine(Patrulha());
            projetilAttack = false;
        }
    }


    IEnumerator Patrulha()
    {
        estaPatrulhando = true;
        while (cutSceneBoss.isFighting)
        {
            if (estaPatrulhando)
            {
                if (isRight)
                {
                    // Move para a direita
                    while (Vector2.Distance(transform.position, pontoDireita.position) > distanciaDeParada)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, pontoDireita.position, velocidade * Time.deltaTime);
                        yield return null;
                    }
                }
                else
                {
                    // Move para a esquerda
                    while (Vector2.Distance(transform.position, pontoEsquerda.position) > distanciaDeParada)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, pontoEsquerda.position, velocidade * Time.deltaTime);
                        yield return null;
                    }
                }
            }

            estaPatrulhando = false;
            yield return new WaitForSeconds(tempoDeEspera);

            isRight = !isRight;
            estaPatrulhando = true;
        }

        estaPatrulhando = false;
    }
}
