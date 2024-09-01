using UnityEngine;
using System.Collections;

public class SpawnerManager2D : MonoBehaviour
{
    [System.Serializable]
    public class SpawnerData
    {
        public Spawner2D spawner; // El spawner asociado
        [Range(0f, 1f)] public float spawnProbability = 0.5f; // Probabilidad de que este spawner genere algo
    }

    public SpawnerData[] spawners; // Lista de spawners con sus probabilidades
    public float checkInterval = 1f; // Intervalo de tiempo entre cada verificaci�n de spawn
    public float multipleSpawnProbability = 0.2f; // Probabilidad de que se activen m�ltiples spawners

    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Iniciar el proceso de verificaci�n de spawns
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        // Detener la rutina de spawn si el objeto es desactivado
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Realizar la verificaci�n de spawns
            CheckForSpawn();

            // Esperar hasta el pr�ximo intervalo de spawn, que puede cambiar din�micamente
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void CheckForSpawn()
    {
        // Generar un n�mero aleatorio entre 0 y 1 para determinar si se generan m�ltiples spawns
        float randomMultiSpawn = Random.Range(0f, 1f);

        if (randomMultiSpawn <= multipleSpawnProbability)
        {
            // Generar m�ltiples spawns
            SpawnMultiple();
        }
        else
        {
            // Generar un solo spawn
            SpawnSingle();
        }
    }

    void SpawnSingle()
    {
        // Generar un n�mero aleatorio entre 0 y 1
        float randomValue = Random.Range(0f, 1f);

        // Determinar cu�l spawner debe activarse seg�n el valor aleatorio
        foreach (SpawnerData spawnerData in spawners)
        {
            if (randomValue <= spawnerData.spawnProbability)
            {
                spawnerData.spawner.Spawn();
                break;
            }
        }
    }

    void SpawnMultiple()
    {
        // Determinar cu�les spawners se activan seg�n sus probabilidades
        foreach (SpawnerData spawnerData in spawners)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= spawnerData.spawnProbability)
            {
                spawnerData.spawner.Spawn();
            }
        }
    }

    // M�todo para actualizar la probabilidad de spawn de un spawner espec�fico
    public void UpdateSpawnProbability(int spawnerIndex, float newProbability)
    {
        if (spawnerIndex >= 0 && spawnerIndex < spawners.Length)
        {
            spawners[spawnerIndex].spawnProbability = newProbability;
        }
    }
}