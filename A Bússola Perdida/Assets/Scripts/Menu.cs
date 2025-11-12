using UnityEngine; 
using UnityEngine.SceneManagement; 

//Classe responsável por controlar o menu e o painel de configurações
public class Menu : MonoBehaviour
{
    //Referência ao painel de configurações (Canvas)
    public GameObject PanelConfig;

    //Referências a elementos da interface (UI)
    //que são escondidos quando o painel de configuração é aberto
    public GameObject imagePocao;
    public GameObject imageRosa;
    public GameObject barraDeVida;

    //Método chamado por um botão para abrir o painel de configurações
    public void AbrirConfig() 
    {
        //Verifica se o painel existe na cena
        if (PanelConfig != null)
        {
            PanelConfig.SetActive(true); //Ativa o painel (Canvas Configurações)

            //Oculta elementos da HUD principal enquanto o painel está aberto
            imagePocao.SetActive(false);
            imageRosa.SetActive(false);
            barraDeVida.SetActive(false);
        }
    }
    
    //Método chamado por um botão para fechar o painel de configurações
    public void FecharConfig()
    {
        if (PanelConfig != null)
        {
            PanelConfig.SetActive(false); //Desativa o painel
        }
    }

    //Método público para carregar uma cena específica
    //Usado em botões de menu (por exemplo: “Jogar”, “Voltar”, etc.)
    public void LoadScene(string cena)
    {
        SceneManager.LoadScene(cena); //Carrega a cena cujo nome foi passado no parâmetro
    }

    //Método chamado para sair do jogo
    //OBS: Funciona apenas no executável final (não no editor da Unity)
    public void Quit()
    {
        Application.Quit(); //Fecha a aplicação
    }
}
