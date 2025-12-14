using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Configurações")]
    public Transform player;          // Referência ao player
    public float parallaxFactor = 0.5f; // Quanto menor, mais distante parece

    private Vector3 lastPlayerPos;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("Parallax: Player não atribuído!");
            enabled = false;
            return;
        }

        lastPlayerPos = player.position;
    }

    void Update()
    {
        // Quanto o player se moveu
        Vector3 playerDelta = player.position - lastPlayerPos;

        // Move o fundo no sentido CONTRÁRIO ao player
        transform.position -= new Vector3(playerDelta.x * parallaxFactor, 0f, 0f);

        // Atualiza a última posição do player
        lastPlayerPos = player.position;
    }
}
