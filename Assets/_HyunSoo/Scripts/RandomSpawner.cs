using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] Mobs;

    public Transform[] spawnPoints;

    public float spawnInterval = 0.5f;

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "OutMap" || currentSceneName == "Dungeon")
        {
            PlaceRandomPrefabsObj();
        }
        else if (currentSceneName == "SciFi_Warehouse")
        {
            InvokeRepeating("PlaceRandomPrefabsMobs", 0f, spawnInterval);
        }
 
    }
    void PlaceRandomPrefabsObj()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, Mobs.Length);
            GameObject selectedPrefab = Mobs[randomIndex];

            Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    void PlaceRandomPrefabsMobs()
    {
        int randindex = Random.Range(0, spawnPoints.Length);
        int randomIndex = Random.Range(0, Mobs.Length);
        Instantiate(Mobs[randomIndex], spawnPoints[randindex]);
    }
}
