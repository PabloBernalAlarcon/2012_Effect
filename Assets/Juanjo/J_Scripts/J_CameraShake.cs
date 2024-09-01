using UnityEngine;
using System.Collections;

public class CameraShake2D : MonoBehaviour
{
    public Transform cameraTransform; // La transform de la c�mara que va a temblar
    public float shakeDuration = 0.5f; // Duraci�n del temblor
    public float shakeMagnitude = 0.1f; // Intensidad del temblor
  

    private Vector3 originalPosition;

    private void OnEnable()
    {
        // Subscribe to the event
        P_GameManager.OnGodTriggerWarning += HandleCamShake;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        P_GameManager.OnGodTriggerWarning -= HandleCamShake;
    }

    // This method will be called when the event is triggered
    private void HandleCamShake()
    {
        StartCoroutine(Shake());
    }

    private void Start()
    {
        // Guardar la posici�n original de la c�mara
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Asignar la c�mara principal si no se ha asignado
        }

        originalPosition = cameraTransform.localPosition;

       
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
     
    }
}