using UnityEngine;
using System.Collections.Generic;

public class PotionPool : MonoBehaviour
{
    public static PotionPool Instance;

    public GameObject potionPrefab;
    public int poolSize = 10;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject potion = Instantiate(potionPrefab);
            potion.SetActive(false);
            pool.Add(potion);
        }
    }

    public GameObject GetPotion()
    {
        foreach (GameObject potion in pool)
        {
            if (!potion.activeInHierarchy)
                return potion;
        }

        // Caso todas estejam em uso (opcional)
        GameObject newPotion = Instantiate(potionPrefab);
        newPotion.SetActive(false);
        pool.Add(newPotion);
        return newPotion;
    }
}
