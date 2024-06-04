using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] Mobs;

    public Transform[] spawnPoints;

    public float spawnInterval = 5f;

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "OutMap")
        {
            PlaceRandomPrefabs();
        }
        else if (currentSceneName == "SciFi_Warehouse")
        {
            InvokeRepeating("PlaceRandomPrefabs", 0f, spawnInterval);
        }
    }

    void PlaceRandomPrefabs()
    {
        /*foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, Mobs.Length);
            GameObject selectedPrefab = Mobs[randomIndex];
            
            Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
        }*/
        int randindex = Random.Range(0, spawnPoints.Length);
        int randomIndex = Random.Range(0, Mobs.Length);
        Transform selectedTrans = spawnPoints[randindex];
        GameObject selectedPrefab = Mobs[randomIndex];

        Instantiate(selectedPrefab, selectedTrans);
    }
}
