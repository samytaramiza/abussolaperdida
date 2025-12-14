using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    void Awake()
    {
        // Garante que começa ativa
        gameObject.SetActive(true);
    }
}
