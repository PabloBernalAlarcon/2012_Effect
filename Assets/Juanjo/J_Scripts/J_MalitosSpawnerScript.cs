using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public Vector2 lineStart = new Vector2(-5f, 0f); // Punto inicial de la l�nea
    public Vector2 lineEnd = new Vector2(5f, 0f); // Punto final de la l�nea
    public float spawnInterval = 3f; // Intervalo de tiempo entre spawns
    private float timeSinceLastSpawn;
    public GameObject papi;
   
    
    void Start()
    {
        
        timeSinceLastSpawn = spawnInterval; // Inicia el tiempo para el primer spawn
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy(enemyPrefab);
            timeSinceLastSpawn = 0f;
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        // Genera una posici�n aleatoria a lo largo de la l�nea
        Vector2 spawnPosition = Vector2.Lerp(lineStart, lineEnd, Random.Range(0f, 1f));

        // Genera el enemigo en la posici�n seleccionada
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity,papi.transform);
    }

    // Dibujar la l�nea de spawn en la escena para facilitar el ajuste
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
