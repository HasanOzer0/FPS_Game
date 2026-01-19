using UnityEngine;
using TMPro;
// Artık Infima'nın silahına direkt erişebiliriz
using InfimaGames.LowPolyShooterPack;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("--- EKONOMİ & UI ---")]
    public int gold = 0;
    public TextMeshProUGUI goldText;
    public GameObject shopPanel;

    [Header("--- GÜÇLENDİRMELER ---")]
    public float damageMultiplier = 1.0f;

    [Header("--- SİLAH BAĞLANTISI ---")]
    public GameObject activeGunObject; // Silah objesini buraya sürükle

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        if (shopPanel) shopPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleShop();
    }

    // --- MERMİ YÜKLEME ---
    public void BuyAmmo()
    {
        if (gold >= 100)
        {
            // Silah objesinden yeni "Weapon" scriptini al
            Weapon weaponScript = activeGunObject.GetComponent<Weapon>();

            if (weaponScript != null)
            {
                gold -= 100;
                weaponScript.storedAmmo += 30; // Direkt cebe mermi ekle!
                UpdateUI();
                Debug.Log("Mermi alındı! Yeni Yedek Mermi: " + weaponScript.storedAmmo);
            }
            else
            {
                Debug.LogError("HATA: Active Gun Object kutusuna yanlış obje sürüklenmiş!");
            }
        }
    }

    // --- DİĞERLERİ ---
    public void BuyDamageUpgrade()
    {
        if (gold >= 500)
        {
            gold -= 500;
            damageMultiplier += 0.5f;
            UpdateUI();
        }
    }

    public void BuyStarterPack()
    {
        gold += 1000;

        Weapon weaponScript = activeGunObject.GetComponent<Weapon>();
        if (weaponScript != null)
        {
            weaponScript.storedAmmo += 90;
        }

        UpdateUI();
        ToggleShop();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    void ToggleShop()
    {
        bool isOpening = !shopPanel.activeSelf;
        shopPanel.SetActive(isOpening);

        Cursor.lockState = isOpening ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpening;

        Time.timeScale = isOpening ? 0f : 1f;
    }

    void UpdateUI()
    {
        if (goldText) goldText.text = "GOLD: $" + gold;
    }
}