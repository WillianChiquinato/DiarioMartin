using UnityEngine;

public class FantasmaEstilingue : MonoBehaviour
{
    public GameObject projetilPrefab;
    public float forcaTiro = 10f;
    public Transform pontoDeTiro;

    public void Atirar(Vector2 direcao)
    {
        GameObject projetil = Instantiate(projetilPrefab, pontoDeTiro.position, Quaternion.identity);
        projetil.GetComponent<Rigidbody2D>().linearVelocity = direcao * forcaTiro;

        //O objeto rotaciona para a direção do tiro.
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        projetil.transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);

        Destroy(this.gameObject, 0.1f);
    }
}
