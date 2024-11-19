using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraEffector : MonoBehaviour
{
    /* 
     * 위아래 카메라 블랙박스 on off
     * 
     * 카메라 진동
     * 
     * 카메라 이벤트 트리거
     */
    [SerializeField]
    private GameObject camera;
    
    
    [SerializeField]
    private GameObject blacker;

    public void BlackBoxSwitch()
    {
        if(blacker.activeSelf)
        {
            blacker.SetActive(false);
        }
        else
        {
            blacker.SetActive(true);
        }
    }
    public void BlackBoxOff()
    {
        blacker.SetActive(false);
    }
    public void BlackBoxOn()
    {
        blacker.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BlackBoxSwitch();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            
        }
    }

    public void CameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            camera.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.localPosition = originalPos;
    }
    
    
    
    
}
