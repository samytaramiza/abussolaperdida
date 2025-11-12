using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    //Referência ao componente Slider (a barra visual de vida na interface)
    public Slider slider;


    //MÉTODO PARA DEFINIR O VALOR MÁXIMO DA VIDA
    //Ele inicializa o valor máximo da vida e já coloca o slider cheio
    public void SetMaxVida(float maxVida)
    {
        slider.maxValue = maxVida; // define o valor máximo do slider
        slider.value = maxVida;    // começa com a barra cheia
    }


    //MÉTODO PARA ATUALIZAR O VALOR DA VIDA
    //Chamado sempre que o personagem sofre dano ou recupera HP
    public void GerenciarVida(float vida)
    {
        // Atualiza a posição da barra conforme a vida atual
        slider.value = vida;
    }
}
