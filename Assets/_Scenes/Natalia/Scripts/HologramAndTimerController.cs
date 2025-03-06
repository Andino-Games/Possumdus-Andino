using UnityEngine;
using TMPro;

public class HologramAndCountdownController : MonoBehaviour
{
    public Renderer hologramRenderer; // Renderer del quad del holograma
    public GameObject reloj; // Objeto que representa el reloj
    public GameObject timerUI; // El contenedor UI del cronómetro (puede ser el Panel o directamente el TextMeshProUGUI)
    public TextMeshProUGUI timerText; // El texto del cronómetro en la UI
    public int countdownTime = 10; // Tiempo en segundos para la cuenta regresiva inicial

    private float timeRemaining; // Tiempo restante de la cuenta regresiva
    private bool isTimerRunning = false; // Estado del cronómetro
    private bool isVisible = false; // Estado de visibilidad del holograma y cronómetro

    void Start()
    {
        // Ocultar el holograma y la UI del cronómetro al inicio
        hologramRenderer.enabled = false;
        timerUI.SetActive(false);
        timeRemaining = countdownTime;
    }

    void Update()
    {
        // Si el cronómetro está en marcha, continuar contando
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            // Evitar que el cronómetro baje de cero
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
            }

            // Actualizar el texto del cronómetro solo si está visible
            if (isVisible)
            {
                UpdateTimerText();
            }
        }
    }

    void OnMouseDown()
    {
        // Verificar si el reloj ha sido tocado
        if (IsLookingAtReloj())
        {
            // Iniciar el cronómetro si aún no ha comenzado
            if (!isTimerRunning)
            {
                isTimerRunning = true;
            }

            // Alternar visibilidad del holograma y cronómetro
            ToggleHologramAndTimerVisibility();
        }
    }

    bool IsLookingAtReloj()
    {
        // Detectar si el clic fue sobre el reloj
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == reloj;
        }
        return false;
    }

    void ToggleHologramAndTimerVisibility()
    {
        // Alternar visibilidad del holograma y cronómetro
        isVisible = !isVisible;
        hologramRenderer.enabled = isVisible;
        timerUI.SetActive(isVisible);

        // Actualizar el texto si el cronómetro está visible
        if (isVisible)
        {
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
