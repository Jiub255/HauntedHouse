using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI garlicText;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private FearManager fearManager;
    [SerializeField]
    private Image fearBarImage;
    [SerializeField]
    private float updateTimerLength = 0.1f;
    private float updateTimer;

    private void OnEnable()
    {
        InventoryManager.onInventoryChanged += UpdateHUD;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        InventoryManager.onInventoryChanged -= UpdateHUD;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        inventoryManager = playerTransform.GetComponent<InventoryManager>();
        fearManager = playerTransform.GetComponent<FearManager>();
    }

    private void Start()
    {
        UpdateHUD();
        UpdateFear();
    }

    private void Update()
    {
        updateTimer -= Time.deltaTime;

        if (updateTimer < 0)
        {
            updateTimer = updateTimerLength;

            UpdateFear();
        }
    }

    public void UpdateHUD()
    {
        UpdateGarlic();
    }

    private void UpdateGarlic()
    {
        foreach (ItemAmount itemAmount in inventoryManager.inventoryList)
        {
            if (itemAmount.item.itemName == "Garlic")
            {
                garlicText.text = itemAmount.amount.ToString();
                return;
            }
        }

        garlicText.text = "0";
    }

    private void UpdateFear()
    {
        fearBarImage.fillAmount = Mathf.Clamp(
            fearManager.currentFear / fearManager.maxFear, 0f, 1f);
    }
}