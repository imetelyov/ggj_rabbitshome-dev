using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QuasarGames
{

    //[ExecuteInEditMode]
    public class CanvasFullscreenEffects : MonoBehaviour
    {
        public static CanvasFullscreenEffects Instance;

        // FadeInOut
        [Header("Fade In Out")]
        public Image fadeInOutImage;
        [Range(0f, 1f)]
        public float fadeInAlpha = 0f;
        [Range(0f, 1f)]
        public float fadeOutAlpha = 1f;
        public float fadeInOutDuration = 0.75f;

        private void Awake()
        {
            Instance = this;
            SetFadeInOutEnabled(true);
            fadeInOutImage.ColorSetAlpha(1f);
        }

        // FADE IN OUT

        private void SetFadeInOutEnabled(bool isEnabled = true)
        {
            fadeInOutImage.enabled = isEnabled;
        }

        public void FadeIn()
        {
            SetFadeInOutEnabled(true);
            DOTween.ToAlpha(() => fadeInOutImage.color, x => fadeInOutImage.color = x, fadeInAlpha, fadeInOutDuration).OnComplete(() => SetFadeInOutEnabled(false));
        }

        public void FadeOut()
        {
            SetFadeInOutEnabled(true);
            DOTween.ToAlpha(() => fadeInOutImage.color, x => fadeInOutImage.color = x, fadeOutAlpha, fadeInOutDuration).OnComplete(() => SetFadeInOutEnabled(true));
        }

    }
}
