using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEvent : MonoBehaviour {

    private GameObject m_Fade;
    public float m_fDuration = 3f;

    void Start()
    {
        m_Fade = this.gameObject;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 1f);
        yield return new WaitForSeconds(m_fDuration);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 0f);
        yield return new WaitForSeconds(m_fDuration);
        StartCoroutine(FadeOut());
    }
}
