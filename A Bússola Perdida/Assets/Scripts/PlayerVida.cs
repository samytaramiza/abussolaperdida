using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    public float vidaMax = 100f; //Quantidade máxima de vida do jogador
    private float vidaAtual; //Vida atual (diminui ao tomar dano)

    public BarraDeVida barraDeVida; //Referência ao componente da barra de vida na interface (UI)

    void Start()
    {
        //Inicializa a vida do jogador no início do jogo
        vidaAtual = vidaMax;

        //Configura a barra de vida com o valor máximo
        barraDeVida.SetMaxVida(vidaMax);
    }

    //Método chamado quando o jogador leva dano
    public void TomarDano(float dano)
    {
        //Diminui a vida de acordo com o valor do dano recebido
        vidaAtual -= dano;

        //Atualiza o valor visual da barra de vida
        barraDeVida.GerenciarVida(vidaAtual);

        //Verifica se a vida chegou a zero (ou menos)
        if (vidaAtual <= 0)
        {
            Morrer(); //Chama o método de morte
        }
    }

    //Método executado quando o jogador morre
    void Morrer()
    {
        Debug.Log("Jogador morreu!");
        //Aqui você pode adicionar:
        // - Animação de morte
        // - Desativar controles do jogador
        // - Chamar tela de Game Over
        // - Reiniciar a fase, etc.
    }
}
