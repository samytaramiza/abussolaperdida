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
        GameController.instance.AlterarVida(-dano);
    }

}
