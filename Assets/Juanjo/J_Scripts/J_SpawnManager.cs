using UnityEngine;

public class SpawnerManager2D : MonoBehaviour
{
    [System.Serializable]
    public class SpawnerData
    {
        public Spawner2D spawner; // El spawner asociado
        public float spawnProbability; // Probabilidad de que este spawner genere algo
        [HideInInspector] public float cumulativeProbability; // Probabilidad acumulada, utilizada para el cálculo
    }

    public SpawnerData[] spawners; // Lista de spawners con sus probabilidades
    public float checkInterval = 1f; // Intervalo de tiempo entre cada verificación de spawn
    public float multipleSpawnProbability = 0.2f; // Probabilidad de que se activen múltiples spawners

    private void Start()
    {
        // Normalizar las probabilidades para que sumen 1
        NormalizeProbabilities();

        // Iniciar el proceso de verificación de spawns
        InvokeRepeating("CheckForSpawn", checkInterval, checkInterval);
    }

    void NormalizeProbabilities()
    {
        float totalProbability = 0f;

        // Sumar todas las probabilidades
        foreach (SpawnerData spawnerData in spawners)
        {
            totalProbability += spawnerData.spawnProbability;
        }

        // Normalizar y calcular la probabilidad acumulada para cada spawner
        float cumulative = 0f;
        foreach (SpawnerData spawnerData in spawners)
        {
            spawnerData.spawnProbability /= totalProbability;
            cumulative += spawnerData.spawnProbability;
            spawnerData.cumulativeProbability = cumulative;
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
            if (randomValue <= spawnerData.cumulativeProbability)
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
}