using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public GameObject objectToSpawn; // El objeto que será generado
    public Transform startPoint; // Punto inicial de la línea
    public Transform endPoint; // Punto final de la línea
    public GameObject papi;
    public Color gizmoColor = Color.green; // Color de la línea en los Gizmos

    public void Spawn()
    {
        // Elegir una posición aleatoria a lo largo de la línea definida por startPoint y endPoint en 2D
        Vector2 spawnPosition = Vector2.Lerp(startPoint.position, endPoint.position, Random.Range(0f, 1f));

        // Generar el objeto en la posición aleatoria
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity,papi.transform);
    }

    // Método para dibujar la línea en la escena utilizando Gizmos
    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            // Establecer el color de los Gizmos
            Gizmos.color = gizmoColor;

            // Dibujar una línea desde startPoint hasta endPoint
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
}