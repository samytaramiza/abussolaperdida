using UnityEngine;

public class Pocoes : MonoBehaviour
{
    //Componentes da poção
    private SpriteRenderer sr; //Controla a imagem do objeto (permite esconder ou mostrar a poção)
    private CircleCollider2D circle; //Detecta colisões com o jogador (usado como "gatilho" para coleta)

    //Valor da poção 
    public int scorePocoes = 1; //Quantidade de poções que o jogador recebe ao coletar

    void Start()
    {
        //Obtém referências aos componentes existentes no mesmo GameObject
        sr = GetComponent<SpriteRenderer>(); //Pega o componente de imagem
        circle = GetComponent<CircleCollider2D>(); //Pega o componente de colisão circular
    }

    // Este método é chamado automaticamente quando outro collider entra na área do trigger
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Verifica se quem encostou foi o jogador
        if (collider2D.CompareTag("Player"))
        {
            //Desativa o sprite — a poção "desaparece" visualmente
            sr.enabled = false;

            //Desativa o collider — evita que o jogador colete a mesma poção mais de uma vez
            circle.enabled = false;

            // Soma o valor da poção ao total do jogador
            GameController.instance.totalScorePocoes += scorePocoes;

            // Atualiza o texto na tela mostrando o novo total de poções
            GameController.instance.UpdateScoreText();

            //Toca o som de coleta da poção
            AudioManager.Instance.PlayAudioPotion();

            //Destroi o objeto após 0.3 segundos (tempo suficiente pro som tocar)
            Destroy(gameObject, 0.3f);
        }
    }
}
