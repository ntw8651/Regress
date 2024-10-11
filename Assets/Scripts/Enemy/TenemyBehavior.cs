using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TenemyBehavior : MonoBehaviour
{
    /*
     * 여기서 Enemy를 상속받는다.
     * 
     * 
     */

    public GameObject body; // 실제 몸체
    private Vector3 originalPosition;
    public float rotatingSpeed = 5;
    public float floatingSpeed = 2.0f;
    public float floatingLength = 0.5f; 
    private void Start()
    {
        originalPosition = body.transform.localPosition;
    }
    void Update()
    {
        // y축 회전
        body.transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);

        // y축 이동운동
        float _newYPosition = originalPosition.y + Mathf.Sin(Time.time * floatingSpeed) * floatingLength;
        body.transform.localPosition = new Vector3(originalPosition.x, _newYPosition, originalPosition.z);

    }
}
