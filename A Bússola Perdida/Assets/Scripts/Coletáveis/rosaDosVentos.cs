using UnityEngine;

public class rosaDosVentos : MonoBehaviour
{
    //Componentes do objeto
    private SpriteRenderer sr; //Para habilitar/desabilitar a imagem da rosa dos ventos
    private CircleCollider2D circle; // Collider usado para detectar colisão com o jogador

    //Quantidade de pontos que a rosa dos ventos dá ao jogador
    public int scoreRosa = 1;
    
    void Start()
    {
        //Obtém referências aos componentes do objeto
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    //Detecta quando o jogador entra no trigger da rosa dos ventos
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            //Desativa visualmente a rosa dos ventos
            sr.enabled = false;
            //Desativa o collider para evitar múltiplas coletas
            circle.enabled = false;

            //Adiciona a quantidade de pontos ao total de moedas/pontos do jogador
            GameController.instance.AddRosa(scoreRosa);

            AudioManager.Instance.PlayAudioCoin();

            //Destroi o objeto após um pequeno delay, permitindo efeitos visuais ou sons
            Destroy(gameObject, 0.3f);
        }
    }
}
