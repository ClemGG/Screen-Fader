using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Script de test du ScreenFader.
//Lance un fondu vers la scène dans la Start, puis attend 1 seconde avant de lancer un fondu vers le noir.

namespace Project.ScreenFader
{
    [RequireComponent(typeof(Camera))]
    public class TestScreenFader : MonoBehaviour
    {
        [Space(10)]
        [Header("Transitions : ")]
        [Space(10)]

        [SerializeField] private ScreenFaderSO m_screenFader;
        [SerializeField] private TransitionSettingsSO m_firstTransition;
        [SerializeField] private TransitionSettingsSO m_secondTransition;

        [Space(10)]
        [Header("Texture Blend : ")]
        [Space(10)]

        [SerializeField] private Image m_backgroundImg;
        [SerializeField] private Texture2D m_firstTexture;
        [SerializeField] private Texture2D m_secondTexture;
        [SerializeField] private TransitionSettingsSO m_blendTransition;


        private void Awake()
        {
            m_backgroundImg.material.SetTexture("_MainTex", m_firstTexture);

            m_screenFader.OnTransitionEnded += StartBlend;
            m_screenFader.StartCompleteFade(this, false, true, Time.unscaledDeltaTime, m_firstTransition, m_secondTransition);
        }

        private void OnDisable()
        {
            m_screenFader.OnTransitionEnded -= StartBlend;
        }


        private void StartBlend()
        {
            m_screenFader.OnTransitionEnded -= StartBlend;
            m_screenFader.StartBlend(m_backgroundImg, Time.unscaledDeltaTime, m_secondTexture, m_blendTransition);
        }
    }
}