using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float zForce = 10f; // ขนาดแรงในแกน Z
    public float interval2Sec = 2f; // เวลารอสำหรับแรง +Z
    public float interval4Sec = 4f; // เวลารอสำหรับแรง -Z
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ApplyForceRoutine());
    }

    IEnumerator ApplyForceRoutine()
    {
        while (true) // ทำงาน無限ループ
        {
            // แรง +Z ทุก 2 วินาที
            rb.AddForce(0, 0, zForce, ForceMode.Impulse);
            yield return new WaitForSeconds(interval2Sec);

            // แรง -Z ทุก 4 วินาที
            rb.AddForce(0, 0, -zForce, ForceMode.Impulse);
            yield return new WaitForSeconds(interval4Sec);
        }
    }
}
