using UnityEngine;

public class ParallaxComPlayer : MonoBehaviour
{
    private float comprimentoSprite;   // Largura do sprite do fundo
    private float posicaoInicialX;     // Posição inicial no eixo X
    private Transform cameraTransform; // Referência à câmera (ou player)
    public float efeitoParallax;       // Intensidade do efeito (quanto menor, mais distante o fundo parece)
    
    void Start()
    {
        posicaoInicialX = transform.position.x;
        comprimentoSprite = GetComponent<SpriteRenderer>().bounds.size.x;

        // Pega a câmera principal (pode trocar pelo player se preferir)
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Movimento relativo da câmera
        float distancia = cameraTransform.position.x * efeitoParallax;

        // Atualiza a posição do fundo
        transform.position = new Vector3(posicaoInicialX + distancia, transform.position.y, transform.position.z);

        // Faz o looping do fundo (caso precise cenário infinito)
        float repeticao = cameraTransform.position.x * (1 - efeitoParallax);
        if (repeticao > posicaoInicialX + comprimentoSprite)
            posicaoInicialX += comprimentoSprite;
        else if (repeticao < posicaoInicialX - comprimentoSprite)
            posicaoInicialX -= comprimentoSprite;
    }
}
