using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endtime : MonoBehaviour
{
    public string nextSceneName = "Menu"; // ���� Scene ��ѡ��������¹�
    public float displayTime = 15f; // �����ʴ� 15 �Թҷ�

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        // ����� Coroutine �Ѻ����
        StartCoroutine(LoadSceneAfterDelay());
        audioManager.PlaySFX(audioManager.endCredit);
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // �͵�����ҷ���˹�
        yield return new WaitForSeconds(displayTime);

        // ��Ǩ�ͺ����� Scene ����ԧ���
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
