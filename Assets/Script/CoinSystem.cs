using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinSystem  : MonoBehaviour
{

    public static CoinSystem Instance;

    [Header("Coin Settings")]
    public int totalCoins = 5; // �ӹǹ����­������㹴�ҹ
    public int collectedCoins = 0; // ����­�������
    public string endSceneName = "EndCredit"; // ���ͫչ����

    [Header("UI Settings")]
    public TextMeshProUGUI coinCounterText; // UI Text ����ʴ��ӹǹ����­
    public GameObject coinEffectPrefab; // �Ϳ࿡�������������­ (Optional)

    void Awake()
    {
        // ��駤�� Singleton ����� Object �١���ҧ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // ����� Instance ��������������� Object ����
        }
    }

    void Start()
    {
        UpdateCoinUI();
    }

    // ���¡�ҡ OnTriggerEnter �ͧ����­
    public void CollectCoin(GameObject coin)
    {
        collectedCoins++;

        // ����Ϳ࿡�� (�����)
        if (coinEffectPrefab != null)
        {
            Instantiate(coinEffectPrefab, coin.transform.position, Quaternion.identity);
        }

        Destroy(coin); // ź����­
        UpdateCoinUI();

        // ��Ǩ�ͺ����纤ú�������
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
        // ��Ǩ�ͺ��ҫչ��������� Build Settings
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
