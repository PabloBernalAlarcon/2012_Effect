using UnityEngine;
using System.Collections;

public class CameraShake2D : MonoBehaviour
{
    public Transform cameraTransform; // La transform de la c�mara que va a temblar
    public float shakeDuration = 0.5f; // Duraci�n del temblor
    public float shakeMagnitude = 0.1f; // Intensidad del temblor
    public float checkInterval = 2f; // Intervalo de tiempo entre cada revisi�n de probabilidad
    public float shakeProbability = 0.3f; // Probabilidad de que ocurra un temblor (0 a 1)

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        // Guardar la posici�n original de la c�mara
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asignar la c�mara principal si no se ha asignado
        }

        originalPosition = cameraTransform.localPosition;

        // Comenzar la revisi�n peri�dica de la probabilidad de temblor
        InvokeRepeating("CheckForShake", checkInterval, checkInterval);
    }

    private void CheckForShake()
    {
        // Generar un n�mero aleatorio entre 0 y 1
        float randomValue = Random.Range(0f, 1f);

        // Si el valor aleatorio es menor o igual a la probabilidad, activar el temblor
        if (randomValue <= shakeProbability && shakeCoroutine == null)
        {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Generar un desplazamiento aleatorio dentro de un c�rculo unitario en 2D y multiplicarlo por la magnitud del temblor
            Vector2 randomOffset = Random.insideUnitCircle * shakeMagnitude;
            cameraTransform.localPosition = new Vector3(originalPosition.x + randomOffset.x, originalPosition.y + randomOffset.y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Restaurar la posici�n original de la c�mara al finalizar el temblor
        cameraTransform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}