using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallDetector : MonoBehaviour
{
    public float deathHeight = -30f; // �����٧�����������ª��Ե
    public string dieSceneName = "Die"; // ���ͫչ������Ŵ

    void Update()
    {
        // ��Ǩ�ͺ���˹�᡹ Y
        if (transform.position.y <= deathHeight)
        {
            LoadDieScene();
        }
    }

    void LoadDieScene()
    {
        // ��Ŵ�չ Die (��ͧ�����ҫչ�������� Build Settings)
        SceneManager.LoadScene(dieSceneName);

        // ������Ẻ Async ����Ѻ�չ�˭�
        // StartCoroutine(LoadDieSceneAsync());
    }

    IEnumerator LoadDieSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dieSceneName);

        // �ͨ����ҫչ����Ŵ����
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
