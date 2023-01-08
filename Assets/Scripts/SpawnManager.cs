using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform baby;
    [SerializeField] Transform player;
    [SerializeField] Transform hideout;
    [SerializeField] GameObject[] ais;
    [SerializeField] Transform[] spawnPoints;
    int animalsSpawned;
    [SerializeField] int totalAnimals;
    bool spawning;
    private void Start()
    {
        StartCoroutine(Spawning());
        GameEvents.instance.onAnimalDie += AnimalDied;
        SpawnAi();
    }
    void AnimalDied()
    {
        if (animalsSpawned < totalAnimals)
        {
            SpawnAi();
        }
        else
        {
            GameEvents.instance.GameOver();
        }
    }
    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(25);
        spawning = animalsSpawned < totalAnimals;
        while (animalsSpawned < totalAnimals)
        {
            float time = Random.Range(10, 15f);
            SpawnAi();
            spawning = animalsSpawned < totalAnimals;
            yield return new WaitForSeconds(time);
        }
    }
    void SpawnAi()
    {
        animalsSpawned++;
        int rand = Random.Range(0, ais.Length);
        int randSpawn = Random.Range(0, spawnPoints.Length);
        AiController ai = Instantiate(ais[rand], spawnPoints[randSpawn].position, Quaternion.identity).GetComponentInChildren<AiController>();
        ai.AssignVars(player, baby, hideout);
    }
}
