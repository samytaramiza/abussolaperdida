using UnityEngine;       
using UnityEngine.UI;     

public class HealthBar : MonoBehaviour
{
    //Referência ao componente Slider que representa a barra de vida
    public Slider slider;

    //Define o valor máximo de vida e inicializa a barra completamente cheia
    public void SetMaxHealth(float max)
    {
        //Configura o valor máximo do slider (vida máxima)
        slider.maxValue = max;
        //Define o valor atual do slider igual ao máximo (vida cheia)
        slider.value = max;
    }

    //Atualiza o valor atual da barra de vida conforme a vida do personagem muda
    public void SetHealth(float current)
    {
        //Ajusta o valor do slider para refletir a vida atual
        slider.value = current;
    }
}
