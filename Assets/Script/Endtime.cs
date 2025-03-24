using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endtime : MonoBehaviour
{
    public string nextSceneName = "Menu"; // ชื่อ Scene หลักที่จะเปลี่ยนไป
    public float displayTime = 15f; // เวลาแสดง 15 วินาที

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        // เริ่ม Coroutine นับเวลา
        StartCoroutine(LoadSceneAfterDelay());
        audioManager.PlaySFX(audioManager.endCredit);
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // รอตามเวลาที่กำหนด
        yield return new WaitForSeconds(displayTime);

        // ตรวจสอบว่ามี Scene นี้จริงไหม
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Scene not found: " + nextSceneName);
        }
    }
}
