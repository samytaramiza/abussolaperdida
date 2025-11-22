using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int life = 100;

    public void TakeDamage(int amount)
    {
        life -= amount;

        if (life <= 0)
            Destroy(gameObject);
    }
}
