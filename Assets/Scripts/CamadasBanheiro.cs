using UnityEngine;

public class CamadasBanheiro : MonoBehaviour
{
    public SpriteRenderer[] camadasBanheiro;

    [SerializeField] private Transform[] areas; // Transforms dos cubos
    [SerializeField] private Vector2[] areaSizes; // Tamanhos dos cubos
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Color gizmoColor = new Color(0, 1, 0, 0.3f); // Verde transparente

    void Awake()
    {
        for (int i = 0; i < camadasBanheiro.Length; i++)
        {
            camadasBanheiro[i].sortingLayerName = "Players";
        }
    }

    private void Update()
    {
        for (int i = 0; i < areas.Length; i++)
        {
            if (i < areaSizes.Length)
            {
                Collider2D playerInArea = Physics2D.OverlapBox(areas[i].position, areaSizes[i], 0, playerLayer);
                if (playerInArea != null)
                {
                    ExecuteAction(i);
                }
            }
        }
    }

    private void ExecuteAction(int areaIndex)
    {
        switch (areaIndex)
        {
            case 0:
                Debug.Log("O jogador entrou na área 0 - Executando ação 1");
                camadasBanheiro[0].sortingOrder = 1;
                break;
            case 1:
                Debug.Log("O jogador entrou na área 1 - Executando ação 2");
                camadasBanheiro[0].sortingOrder = 10;
                camadasBanheiro[1].sortingOrder = 1;
                break;
            case 2:
                Debug.Log("O jogador entrou na área 2 - Executando ação 3");
                camadasBanheiro[1].sortingOrder = 10;
                camadasBanheiro[2].sortingOrder = 1;
                break;
            default:
                Debug.Log("Área desconhecida");
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (areas == null || areaSizes == null) return;

        Gizmos.color = gizmoColor;
        for (int i = 0; i < areas.Length; i++)
        {
            if (i < areaSizes.Length)
            {
                Gizmos.DrawCube(areas[i].position, new Vector3(areaSizes[i].x, areaSizes[i].y, 1f));
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(areas[i].position, new Vector3(areaSizes[i].x, areaSizes[i].y, 1f));
            }
        }
    }
}
