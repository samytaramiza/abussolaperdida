using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Configurações")]
    public Transform player;   // Referência ao player
    public float parallaxFactor = 0.5f; // 0.1 = bem lento / 0.9 = quase igual ao player

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
        // Diferença que o player se moveu desde o último frame
        Vector3 playerDelta = player.position - lastPlayerPos;

        // Move o background apenas na horizontal
        transform.position += new Vector3(playerDelta.x * parallaxFactor, 0f, 0f);

        // Atualiza referência
        lastPlayerPos = player.position;
    }
}
