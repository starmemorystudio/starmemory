using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Black : MonoBehaviour
{
    [Header("基础设置")]
    public AnimationCurve fadeCurve;   // 控制透明度变化曲线（X轴0~1对应时间，Y轴0~1对应透明度）
    public Image targetImage;          // 需要控制透明度的Image组件

    [Header("速度参数")]
    [Range(0.5f, 5f)] 
    public float fadeSpeed = 1f;       // 渐变速度（数值越大越快）

    private Coroutine activeFadeCoroutine; // 当前活动的渐变协程
    private Color initialColor;            // 初始颜色缓存

    void Awake()
    {
        // 自动获取Image组件（如果未手动指定）
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        // 记录初始颜色状态
        initialColor = targetImage.color;
    }

    // ==================== 公开控制方法 ====================
    #region 外部调用接口
    
    /// <summary> 立即变黑（无渐变） </summary>
    public void SetToBlack()
    {
        StopActiveFade();
        SetAlpha(1f);
    }

    /// <summary> 立即变透明（无渐变） </summary>
    public void SetToTransparent()
    {
        StopActiveFade();
        SetAlpha(0f);
    }

    /// <summary> 渐入效果（透明 -> 黑）</summary>
    public void StartFadeIn()
    {
        StartFade(targetAlpha: 1f);
    }

    /// <summary> 渐出效果（黑 -> 透明）</summary>
    public void StartFadeOut()
    {
        StartFade(targetAlpha: 0f);
    }

    #endregion
    // ==================== 核心逻辑 ====================

    // 启动新的渐变（自动停止旧渐变）
    private void StartFade(float targetAlpha)
    {
        // 停止正在进行的渐变
        StopActiveFade();
        
        // 启动新协程并记录引用
        activeFadeCoroutine = StartCoroutine(FadeCoroutine(targetAlpha));
    }

    // 渐变协程核心逻辑
    private IEnumerator FadeCoroutine(float targetAlpha)
    {
        float timer = 0f;
        Color startColor = targetImage.color;
        Color endColor = startColor;
        endColor.a = targetAlpha;

        // 计算需要渐变的时间（根据曲线长度自动适配）
        float duration = GetCurveDuration();

        while (timer < duration)
        {
            timer += Time.deltaTime * fadeSpeed;
            float progress = Mathf.Clamp01(timer / duration);
            float curveValue = fadeCurve.Evaluate(progress);
            
            targetImage.color = Color.Lerp(startColor, endColor, curveValue);
            yield return null;
        }

        // 确保最终颜色准确
        targetImage.color = endColor;
        activeFadeCoroutine = null;
    }

    // ==================== 工具方法 ====================
    
    // 停止当前正在进行的渐变
    private void StopActiveFade()
    {
        if (activeFadeCoroutine != null)
        {
            StopCoroutine(activeFadeCoroutine);
            activeFadeCoroutine = null;
        }
    }

    // 直接设置透明度（不经过渐变）
    private void SetAlpha(float alpha)
    {
        Color newColor = targetImage.color;
        newColor.a = Mathf.Clamp01(alpha);
        targetImage.color = newColor;
    }

    // 自动计算曲线持续时间（根据曲线最后一个关键帧时间）
    private float GetCurveDuration()
    {
        if (fadeCurve.length == 0) return 1f;
        return fadeCurve[fadeCurve.length - 1].time;
    }

    // ==================== 兼容旧系统 ====================
    // 以下方法用于兼容原有代码，新项目建议使用上方的新接口
    
    void OnEnable() => StartFadeOut(); // 启用时自动渐出
    void OnDisable() => StartFadeIn(); // 禁用时自动渐入

    // 兼容旧方法（不建议新代码使用）
    public IEnumerator BlackStart() => FadeCoroutine(0f);
    public IEnumerator BlackOver() => FadeCoroutine(1f);
}