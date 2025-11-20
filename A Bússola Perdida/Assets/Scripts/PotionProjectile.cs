using UnityEngine;

public class PotionProjectile : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
