using UnityEngine;

public class BossPower : MonoBehaviour
{
    public float lifeTime = 3f; // Tempo de vida do poder
    public GameObject hitEffect; // Efeito visual quando acerta algo

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
