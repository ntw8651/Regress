using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public bool IsTransparent { get; private set; } = false; // 객체가 투명한지 여부
    public float speed = 10f;

    private MeshRenderer[] _renderers; // 객체의 MeshRenderer 배열
    private WaitForSeconds _delay = new WaitForSeconds(0.001f); // 짧은 대기 시간
    private WaitForSeconds _resetDelay = new WaitForSeconds(0.005f); // 리셋 대기 시간
    private const float ThresholdAlpha = 0.25f; // 알파값 임계치
    private const float ThresholdMaxTimer = 0.5f; // 최대 타이머 임계치

    private bool _isReseting = false; // 리셋 중인지 여부
    private float _timer = 0f; // 타이머
    private Coroutine _timeCheckCoroutine; // 타이머 체크 코루틴
    private Coroutine _resetCoroutine; // 리셋 코루틴
    private Coroutine _becomeTransparentCoroutine; // 투명화 코루틴

    void Start()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>(); // 자식 객체의 MeshRenderer 가져오기
    }

    /// <summary>
    /// 객체를 투명하게 만드는 메소드
    /// </summary>
    public void BecomeTransparent()
    {
        // 투명 상태이면 리턴
        if (IsTransparent)
        {
            _timer = 0f; // 타이머 초기화
            return;
        }

        // 리셋 중이면 리셋 중지
        if (_resetCoroutine != null && _isReseting)
        {
            _isReseting = false; // 리셋 중지
            IsTransparent = false; // 투명 상태 해제
            StopCoroutine(_resetCoroutine); // 리셋 코루틴 중지
        }

        SetMaterialTransparent(); // 재질을 투명하게 설정
        IsTransparent = true; // 투명 상태 설정
        _becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCoroutine()); // 투명화 코루틴 시작
    }

    #region #Run-time 중에 RenderingMode 바꾸는 메소드들
    // Runtime 중에 RenderingMode를 바꾸는 방법을 찾아보니, 다음과 같은 코드를 사용한다고 함.
    private void SetMaterialRenderingMode(Material material, float mode, int renderQueue, float zWrite)
    {
        material.SetFloat("_Surface", mode); // Surface Type 설정 (0f: Opaque, 1f: Transparent)
        material.SetFloat("_ZWrite", zWrite); // ZWrite 설정
    
        if (mode == 1f) // 투명 모드
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); // 소스 블렌드 설정
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); // 대상 블렌드 설정
            material.EnableKeyword("_ALPHABLEND_ON");
        }
        else // 불투명 모드
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One); // 소스 블렌드 설정 (불투명)
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero); // 대상 블렌드 설정 (불투명)
            material.DisableKeyword("_ALPHABLEND_ON");
        }
    
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = renderQueue; // 렌더 큐 설정
    }

    private void SetMaterialTransparent()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            foreach (Material material in _renderers[i].materials)
            {
                Material instanceMaterial = _renderers[i].material; // 인스턴스 재질
                SetMaterialRenderingMode(instanceMaterial, 1f, 3000, 0f); // 투명 모드, ZWrite 비활성화
            }
        }
    }

    private void SetMaterialOpaque()
    {
        
        for (int i = 0; i < _renderers.Length; i++)
        {
            foreach (Material material in _renderers[i].materials)
            {
                Material instanceMaterial = _renderers[i].material; // 인스턴스 재질
                SetMaterialRenderingMode(instanceMaterial, 0f, -1, 1f); // 불투명 모드, ZWrite 활성화
            }
        }
        
    }

    #endregion

    public void ResetOriginalTransparent()
    {
        _resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine()); // 리셋 코루틴 시작
    }

    private IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true; // 완료 여부

            for(int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i].material.color.a > ThresholdAlpha)
                    isComplete = false; // 알파값이 임계치보다 크면 완료되지 않음

                Color color = _renderers[i].material.color;
                color.a = Mathf.Clamp(color.a - Time.deltaTime, 0f, 1f); // 알파값 감소
                _renderers[i].material.color = color; // 색상 설정
            }

            if (isComplete)
            {
                CheckTimer(); // 타이머 체크
                break;
            }

            yield return _delay; // 짧은 대기
        }
    }

    private IEnumerator ResetOriginalTransparentCoroutine()
    {
        IsTransparent = false; // 투명 상태 해제

        while (true)
        {
            bool isComplete = true; // 완료 여부

            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i].material.color.a < 1f)
                    isComplete = false; // 알파값이 1보다 작으면 완료되지 않음

                Color color = _renderers[i].material.color;
                color.a = Mathf.Clamp(color.a + Time.deltaTime * speed, 0f, 1f); // 알파값 증가
                _renderers[i].material.color = color; // 색상 설정
            }

            if (isComplete)
            {
                _isReseting = false; // 리셋 중지
                break;
            }

            yield return _resetDelay; // 리셋 대기
        }
        
        SetMaterialOpaque(); // 재질을 불투명하게 설정
    }

    public void CheckTimer()
    {
        if (_timeCheckCoroutine != null)
            StopCoroutine(_timeCheckCoroutine); // 타이머 체크 코루틴 중지
        _timeCheckCoroutine = StartCoroutine(CheckTimerCouroutine()); // 타이머 체크 코루틴 시작
    }
    
    private IEnumerator CheckTimerCouroutine()
    {
        _timer = 0f; // 타이머 초기화

        while (true)
        {
            _timer += Time.deltaTime; // 타이머 증가

            // 최대 타이머를 넘으면 리셋 시작
            if(_timer > ThresholdMaxTimer)
            {
                _isReseting = true; // 리셋 시작
                ResetOriginalTransparent(); // 원래 투명 상태로 리셋
                break;
            }

            yield return null; // 다음 프레임까지 대기
        }
    }
}