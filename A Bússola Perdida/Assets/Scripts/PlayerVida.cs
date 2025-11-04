using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    public float vidaMax = 100f;
    private float vidaAtual;
    public BarraDeVida barraDeVida;

    void Start()
    {
        vidaAtual = vidaMax;
        barraDeVida.SetMaxVida(vidaMax);
    }

    public void TomarDano(float dano)
    {
        vidaAtual -= dano;
        barraDeVida.GerenciarVida(vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Jogador morreu!");
        // Aqui você pode carregar uma cena de Game Over ou reiniciar o nível
    }
}
