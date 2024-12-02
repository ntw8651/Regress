using BOKI.LowPolyNature.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffector : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private GameObject blacker;

    [SerializeField]
    private GameObject blackScreen;
    
    public void BlackBoxSwitch()
    {
        if (blacker.activeSelf)
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
            CameraShake(1f, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            FadeOut(1f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            FadeIn(1f);
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

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCorutine(duration));
    }
    public IEnumerator FadeOutCorutine(float duration)
    {
        Color color = blackScreen.GetComponent<Image>().color;
        float startAlpha = color.a;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            color.a = Mathf.Lerp(startAlpha, 0, progress);
            blackScreen.GetComponent<Image>().color = color;
            progress += rate * Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        blackScreen.GetComponent<Image>().color = color;
    }

    
    public void FadeIn(float duration)
    {
        StartCoroutine(FadeInCorutine(duration));
    }
    public IEnumerator FadeInCorutine(float duration)
    {
        Color color = blackScreen.GetComponent<Image>().color;
        float startAlpha = color.a;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            color.a = Mathf.Lerp(startAlpha, 1, progress);
            blackScreen.GetComponent<Image>().color = color;
            progress += rate * Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        blackScreen.GetComponent<Image>().color = color;
    }
}