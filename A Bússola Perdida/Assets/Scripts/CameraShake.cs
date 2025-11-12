using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    //CORROTINA DE TREMORES
    //Dura 'duration' segundos e treme a câmera com intensidade 'magnitude'
    public IEnumerator Shake(float duration, float magnitude)
    {
        //Guarda a posição original da câmera
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f; //tempo passado desde o início do tremor

        // Enquanto a duração não acabar...
        while (elapsed < duration)
        {
            //Gera deslocamentos aleatórios nos eixos X e Y
            //multiplicados pela intensidade do tremor
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            //Aplica a nova posição "tremida"
            transform.localPosition = new Vector3(
                originalPos.x + offsetX,
                originalPos.y + offsetY,
                originalPos.z
            );

            //Avança o tempo e espera o próximo frame
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Quando termina o tremor, volta a posição normal da câmera
        transform.localPosition = originalPos;
    }
}
