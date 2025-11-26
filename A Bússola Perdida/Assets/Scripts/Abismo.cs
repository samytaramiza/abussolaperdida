using UnityEngine;

public class Abismo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameController.instance.PlayerCaiuNoAbismo();
        }
    }
}
