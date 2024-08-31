using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public GameObject objectToSpawn; // El objeto que ser� generado
    public Transform startPoint; // Punto inicial de la l�nea
    public Transform endPoint; // Punto final de la l�nea
    public GameObject papi;
    public Color gizmoColor = Color.green; // Color de la l�nea en los Gizmos

    public void Spawn()
    {
        // Elegir una posici�n aleatoria a lo largo de la l�nea definida por startPoint y endPoint en 2D
        Vector2 spawnPosition = Vector2.Lerp(startPoint.position, endPoint.position, Random.Range(0f, 1f));

        // Generar el objeto en la posici�n aleatoria
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity,papi.transform);
    }

    // M�todo para dibujar la l�nea en la escena utilizando Gizmos
    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            // Establecer el color de los Gizmos
            Gizmos.color = gizmoColor;

            // Dibujar una l�nea desde startPoint hasta endPoint
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}