using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] Mobs;

    public Transform[] spawnPoints;

    public float spawnInterval = 0.5f;

    public int maxMobs = 9;
    int giants = 2;
    int noeys = 4;
    int collectBugs = 3;
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
<<<<<<< HEAD
        else if (currentSceneName == "InsideMap")
        {
            print("inside");
            InvokeRepeating("PlaceRandomButMaxEach", 0f, spawnInterval);
        }
=======
 
>>>>>>> 51092c3d30ee00003db66c72e61bcb2c814ef100
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
    void PlaceRandomButMaxEach()
    {
        if (maxMobs == 0)
            return;

        int randindex = Random.Range(0, spawnPoints.Length);
        int randomIndex = Random.Range(0, Mobs.Length);
        
        if(randindex == 0 && collectBugs != 0)
        {
            Instantiate(Mobs[randomIndex], spawnPoints[randindex]);
            collectBugs--;
            maxMobs--;
        }
        if (randindex == 1 && noeys != 0)
        {
            Instantiate(Mobs[randomIndex], spawnPoints[randindex]);
            noeys--;
            maxMobs--;
        }
        if (randindex == 2 && giants != 0)
        {
            Instantiate(Mobs[randomIndex], spawnPoints[randindex]);
            giants--;
            maxMobs--;
        }
    }
}
