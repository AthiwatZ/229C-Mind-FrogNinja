using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallDetector : MonoBehaviour
{
    public float deathHeight = -30f; // ความสูงที่ถือว่าเสียชีวิต
    public string dieSceneName = "Die"; // ชื่อซีนที่จะโหลด

    void Update()
    {
        // ตรวจสอบตำแหน่งแกน Y
        if (transform.position.y <= deathHeight)
        {
            LoadDieScene();
        }
    }

    void LoadDieScene()
    {
        // โหลดซีน Die (ต้องแน่ใจว่าซีนนี้อยู่ใน Build Settings)
        SceneManager.LoadScene(dieSceneName);

        // หรือใช้แบบ Async สำหรับซีนใหญ่
        // StartCoroutine(LoadDieSceneAsync());
    }

    IEnumerator LoadDieSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dieSceneName);

        // รอจนกว่าซีนจะโหลดเสร็จ
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
