using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float fadeTime = 5f; // duración del fading en segundos
    private new Renderer renderer;


    void Start()
    {
        renderer = GetComponent<Renderer>();
        Material transparentMat = Resources.Load<Material>("Materials/Alpha");
        renderer.material = transparentMat; // Asi
        StartCoroutine(WaitAndFade());
    }

    IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(5f); // Esperar 2 segundos
        StartCoroutine(FadeObject()); // Iniciar el fading
    }

    IEnumerator FadeObject()
    {
        float elapsedTime = 0f;
        Color startColor = renderer.material.color;
        Color endColor = new(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeTime)
        {
            renderer.material.color = Color.Lerp(startColor, endColor, (elapsedTime / fadeTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}