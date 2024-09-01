using UnityEngine;
using System.Collections;
//using static UnityEditor.Progress;

public class SpawnerManager2D : MonoBehaviour
{
    [System.Serializable]
    public class SpawnerData
    {
        public Spawner2D spawner; // El spawner asociado
        [Range(0f, 1f)] public float spawnProbability = 0.5f; // Probabilidad de que este spawner genere algo
    }

    public SpawnerData[] spawners; // Lista de spawners con sus probabilidades
    public float checkInterval = 1f; // Intervalo de tiempo entre cada verificación de spawn
    public float multipleSpawnProbability = 0.2f; // Probabilidad de que se activen múltiples spawners

    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        // Subscribe to the event
        P_BackgroundChangeController.OnBGSequenceOver += triggerDifficulty;

    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnGodTriggerWarning -= triggerDifficulty;

        // Detener la rutina de spawn si el objeto es desactivado
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    // This method will be called when the event is triggered
    private void triggerDifficulty()
    {
        checkInterval -= 0.2f;
        multipleSpawnProbability += 0.1f;

    }

    private void Start()
    {
        // Iniciar el proceso de verificación de spawns
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

   

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Realizar la verificación de spawns
            CheckForSpawn();

            // Esperar hasta el próximo intervalo de spawn, que puede cambiar dinámicamente
            yield return new WaitForSeconds(checkInterval);
        }
    }

    void CheckForSpawn()
    {
        // Generar un número aleatorio entre 0 y 1 para determinar si se generan múltiples spawns
        float randomMultiSpawn = Random.Range(0f, 1f);

        if (randomMultiSpawn <= multipleSpawnProbability)
        {
            // Generar múltiples spawns
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
        // Generar un número aleatorio entre 0 y 1
        float randomValue = Random.Range(0f, 1f);

        // Determinar cuál spawner debe activarse según el valor aleatorio
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
        // Determinar cuáles spawners se activan según sus probabilidades
        foreach (SpawnerData spawnerData in spawners)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= spawnerData.spawnProbability)
            {
                spawnerData.spawner.Spawn();
            }
        }
    }

    // Método para actualizar la probabilidad de spawn de un spawner específico
    public void UpdateSpawnProbability(int spawnerIndex, float newProbability)
    {
        if (spawnerIndex >= 0 && spawnerIndex < spawners.Length)
        {
            spawners[spawnerIndex].spawnProbability = newProbability;
        }
    }
}