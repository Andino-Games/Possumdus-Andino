using _scripts.Player;
using _scripts.UI;
using System.Collections;
using UnityEngine;
using TMPro;

public class FoodDispensator : MonoBehaviour
{

    [Header("Vending Machine Settings")]
    public HungerManager hungerManager;
    public GameObject foodPrefab;  // Prefab de la comida a generar
    public Transform spawnPoint;   // Punto donde se generará la comida
    public float cooldownTime = 2f; // Tiempo de espera entre usos
    public ProgressSlider proSlider;
    public GameObject interactableText;
    public GameObject reticle;
    public float requiredHoldTime = 2f; // Tiempo necesario para llenar el slider
    private bool _playerInRange = false;
    private bool _canDispense = true;
    private bool _isInteracting = false;

    private void Start()
    {
        proSlider.InitializeSlider(requiredHoldTime);
    }

    private void Update()
    {
        if (_playerInRange && _canDispense)
        {
            if (Input.GetKey(KeyCode.E) && !_isInteracting)
            {
                proSlider.gameObject.SetActive(true);
                bool isComplete = proSlider.IncrementProgress(Time.deltaTime);

                if (isComplete)
                {
                    StartCoroutine(DispenseFood());
                    _isInteracting = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                ResetInteraction();
            }
        }
    }

    private IEnumerator DispenseFood()
    {
        _canDispense = false;
        Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Comida dispensada!");

        yield return new WaitForSeconds(cooldownTime);
        _canDispense = true;
        ResetInteraction();
    }

    private void ResetInteraction()
    {
        _isInteracting = false;
        proSlider.ResetProgress();
        proSlider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            interactableText.SetActive(true);
            reticle.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            interactableText.SetActive(false);
            ResetInteraction();
            reticle.SetActive(true);
        }
    }
}

    
    /* public HungerManager hungerManager;
     public GameObject hungryText;
     public GameObject hungryFab;

     void Start()
     {
         hungerManager = FindObjectOfType<HungerManager>();
         if (hungerManager == null)
             Debug.LogError("Hunger Manager script not found in the scene.");
     }

     private void OnTriggerEnter(Collider other)
     {
         if(other.gameObject.CompareTag("Player"))
         {
             if (hungerManager != null)
             {
                 if (hungerManager._isHungry == true)
                 {
                     hungerManager.Eat();
                 }
                 if (hungerManager._isHungry == false)
                 {
                     hungryText.gameObject.SetActive(true);
                     Debug.Log("youre not hungry!!");
                     StartCoroutine(WaitToDeactivateHungryText());
                 }
             }
         }
     }
     private IEnumerator WaitToDeactivateHungryText()
     {
         yield return new WaitForSecondsRealtime(3);
         hungryText.gameObject.SetActive(false);
     [Header("UI")]
    public ProgressSlider proSlider;
    public Character character;
    public GameObject interactableText;
    public GameObject reticle;
     }*/


