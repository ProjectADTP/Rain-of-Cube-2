using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour
{
    private Material _targetMaterial;
    private Coroutine _currentCoroutine;

    public void Initialize(Material material)
    {
        _targetMaterial = material;
    }

    public void ChangeToRandomColor()
    {
        Color newColor = Random.ColorHSV();

        newColor.a = newColor.maxColorComponent;

        _targetMaterial.color = newColor;
    }

    public void ChangeAlpha(float targetAlpha, float fadeTime) 
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeAlphaChanel(targetAlpha, fadeTime));
    }

    public IEnumerator ChangeAlphaChanel(float targetAlpha, float fadeTime)
    {
        Color currentColor = _targetMaterial.color;

        float startAlpha = currentColor.a;

        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);

            currentColor.a = newAlpha;
            _targetMaterial.color = currentColor;

            yield return null;
        }

        currentColor.a = targetAlpha;
        _targetMaterial.color = currentColor;

        _currentCoroutine = null;
    }
}