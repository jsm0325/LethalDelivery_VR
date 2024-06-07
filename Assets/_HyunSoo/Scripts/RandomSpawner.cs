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
    int i = 0;
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
        else if (currentSceneName == "InsideMap")
        {
            Shuffle(spawnPoints);
            InvokeRepeating("PlaceRandomButMaxEach", 0f, spawnInterval);
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
    void PlaceRandomButMaxEach()
    {
        int randindex = Random.Range(0, Mobs.Length);
        if(randindex == 0 && collectBugs > 0)
        {
            Instantiate(Mobs[0], spawnPoints[i]);
            collectBugs--;
            maxMobs--;
            i++;
        }
        if (randindex == 1 && noeys > 0)
        {
            Instantiate(Mobs[1], spawnPoints[i]);
            noeys--;
            maxMobs--;
            i++;
        }
        if (randindex == 2 && giants > 0)
        {
            Instantiate(Mobs[2], spawnPoints[i]);
            giants--;
            maxMobs--;
            i++;
        }
        if (maxMobs == 0)
            return;
    }
    void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
