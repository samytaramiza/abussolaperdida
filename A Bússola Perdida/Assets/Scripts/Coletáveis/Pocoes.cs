using UnityEngine;

public class Pocoes : MonoBehaviour
{
    //Componentes da poção
    private SpriteRenderer sr; // Para habilitar/desabilitar a imagem da poção
    private CircleCollider2D circle; // Collider usado para detectar colisão com o jogador

    //Quantidade de poções que o jogador recebe ao coletar
    public int scorePocoes = 1;
    
    void Start()
    {
        //Obtém referências aos componentes do objeto
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    //Detecta quando o jogador entra no trigger da poção
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            //Desativa visualmente a poção
            sr.enabled = false;

            //Desativa o collider para que o jogador não possa coletar novamente
            circle.enabled = false;

            //Adiciona a quantidade de poções ao total do jogador
            GameController.instance.totalScorePocoes += scorePocoes;

            //Atualiza o texto de UI com o novo total de poções
            GameController.instance.UpdateScoreText();

            AudioManager.Instance.PlayAudioPotion();

            //Destroi o objeto após um pequeno delay, permitindo efeitos visuais ou som
            Destroy(gameObject, 0.3f);
        }
    }
}
