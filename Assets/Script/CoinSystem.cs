using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinSystem  : MonoBehaviour
{

    public static CoinSystem Instance;

    [Header("Coin Settings")]
    public int totalCoins = 5; // จำนวนเหรียญทั้งหมดในด่าน
    public int collectedCoins = 0; // เหรียญที่เก็บได้
    public string endSceneName = "EndCredit"; // ชื่อซีนจบเกม

    [Header("UI Settings")]
    public TextMeshProUGUI coinCounterText; // UI Text ที่แสดงจำนวนเหรียญ
    public GameObject coinEffectPrefab; // เอฟเฟกต์เมื่อเก็บเหรียญ (Optional)

    void Awake()
    {
        // ตั้งค่า Singleton เมื่อ Object ถูกสร้าง
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // ถ้ามี Instance อยู่แล้วให้ทำลาย Object ใหม่
        }
    }

    void Start()
    {
        UpdateCoinUI();
    }

    // เรียกจาก OnTriggerEnter ของเหรียญ
    public void CollectCoin(GameObject coin)
    {
        collectedCoins++;

        // เล่นเอฟเฟกต์ (ถ้ามี)
        if (coinEffectPrefab != null)
        {
            Instantiate(coinEffectPrefab, coin.transform.position, Quaternion.identity);
        }

        Destroy(coin); // ลบเหรียญ
        UpdateCoinUI();

        // ตรวจสอบว่าเก็บครบหรือไม่
        if (collectedCoins >= totalCoins)
        {
            LoadEndScene();
        }
    }

    void UpdateCoinUI()
    {
        coinCounterText.text = $"{collectedCoins}/{totalCoins}";
    }

    void LoadEndScene()
    {
        // ตรวจสอบว่าซีนจบมีอยู่ใน Build Settings
        if (Application.CanStreamedLevelBeLoaded(endSceneName))
        {
            SceneManager.LoadScene(endSceneName);
        }
        else
        {
            Debug.LogError("End scene not found in Build Settings!");
        }
    }
}
