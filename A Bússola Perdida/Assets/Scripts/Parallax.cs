using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;        // Largura do sprite de fundo
    private float startPos;      // Posição inicial no eixo X
    public GameObject player;    // Referência ao jogador
    public float parallaxSpeed;  // Velocidade do efeito (camadas distantes = valores menores)

    private float lastPlayerX;   // Última posição X registrada do player

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        lastPlayerX = player.transform.position.x;
    }

    void Update()
    {
        // Calcula o quanto o player se moveu desde o último frame
        float deltaX = player.transform.position.x - lastPlayerX;
        lastPlayerX = player.transform.position.x;

        // Move o fundo proporcionalmente ao movimento do player
        transform.position += Vector3.right * (deltaX * parallaxSpeed);

        // Calcula a posição relativa do fundo ao player
        float temp = player.transform.position.x * (1 - parallaxSpeed);

        // Cria o loop quando o player ultrapassa o tamanho do sprite
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;

        transform.position = new Vector3(startPos + temp * parallaxSpeed, transform.position.y, transform.position.z);
    }
}
