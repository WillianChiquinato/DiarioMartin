using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Pontos do patrulha")]
    public Animator anim;
    public Transform pontoDireita;
    public Transform pontoEsquerda;
    public PlayerInputSystem playerInputSystem;
    public CutSceneBoss cutSceneBoss;


    [Header("Configurações do patrulha")]
    public float velocidade = 4f;
    public float tempoDeEspera = 1f;
    public float distanciaDeParada = 0.1f;
    public float AttackCooldown;
    public float AttackCooldownTarget;

    public Coroutine patrulhaCoroutine;
    public bool estaPatrulhando = false;
    public bool isRight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerInputSystem = FindFirstObjectByType<PlayerInputSystem>();
        cutSceneBoss = FindFirstObjectByType<CutSceneBoss>();
    }

    void Update()
    {

        if (cutSceneBoss.isFighting)
        {
            AttackCooldown += Time.deltaTime;
            if (!estaPatrulhando)
            {
                patrulhaCoroutine = StartCoroutine(Patrulha());
                estaPatrulhando = true;
            }
        }

        if (AttackCooldown >= AttackCooldownTarget)
        {
            AttackCooldown = 0;
            // Aqui você pode adicionar a lógica de ataque do boss
            StopAllCoroutines();
            Debug.Log("Atacando o jogador!");
            StartCoroutine(AttackTESTE());
            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            // {
            //     Debug.Log("Ataque finalizado");
            //     StartCoroutine(Patrulha());
            //     estaPatrulhando = true;
            // }
        }
    }

    IEnumerator AttackTESTE()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Ataque finalizado");
        StartCoroutine(Patrulha());
        estaPatrulhando = true;
    }

    IEnumerator Patrulha()
    {
        while (cutSceneBoss.isFighting)
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

            yield return new WaitForSeconds(tempoDeEspera);
            isRight = !isRight; // inverte a direção
        }

        estaPatrulhando = false;
    }

}
