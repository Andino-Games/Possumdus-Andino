using _scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class HungerSlider : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider hungerSlider; // Referencia al Slider de la UI

    private HungerManager _hungerManager;

    private void Start()
    {
        // Buscamos el componente HungerManager en el jugador
        _hungerManager = FindObjectOfType<HungerManager>();

        if (_hungerManager == null)
        {
            Debug.LogError("HungerManager no encontrado en la escena.");
            return;
        }

        // Configuramos los valores iniciales del Slider
        hungerSlider.maxValue = _hungerManager.sacietyDuration;
        hungerSlider.value = hungerSlider.maxValue;
    }

    private void Update()
    {
        if (_hungerManager != null)
        {
            // Actualizar el valor del Slider con el tiempo restante antes de tener hambre
            hungerSlider.value = _hungerManager._timeUntilHunger;
        }
    }
}
    

    
