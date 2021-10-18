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

        [SerializeField] private ScreenFaderSO _screenFader;
        [SerializeField] private TransitionSettingsSO _firstTransition;
        [SerializeField] private TransitionSettingsSO _secondTransition;

        [Space(10)]
        [Header("Texture Blend : ")]
        [Space(10)]

        [SerializeField] private Image _backgroundImg;
        [SerializeField] private Texture2D _firstTexture;
        [SerializeField] private Texture2D _secondTexture;
        [SerializeField] private TransitionSettingsSO _blendTransition;


        private void Awake()
        {
            _backgroundImg.material.SetTexture("_MainTex", _firstTexture);


            //On récupère toutes les caméras de la scène pour pouvoir récupérer celle en cours d'utilisation pour les transitions
            _screenFader.GetAllCamerasInScene();

            _screenFader.OnTransitionEnded += StartBlend;
            _screenFader.StartCompleteFade(this, false, true, Time.unscaledDeltaTime, _firstTransition, _secondTransition);

        }

        private void OnDisable()
        {
            _screenFader.OnTransitionEnded -= StartBlend;
        }


        private void StartBlend()
        {
            _screenFader.OnTransitionEnded -= StartBlend;
            _screenFader.StartBlend(_backgroundImg, Time.unscaledDeltaTime, _secondTexture, _blendTransition);
        }
    }
}