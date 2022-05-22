// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;

namespace Tobii.XR.Examples.GettingStarted
{
    //Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
    public class HighlightAtGaze : MonoBehaviour, IGazeFocusable
    {
        //private static readonly int _baseColor = Shader.PropertyToID("_BaseColor");
        //public Color highlightColor = Color.red;
        //public float animationTime = 0.1f;

        //private Renderer _renderer;
        //private Color _originalColor;
        //private Color _targetColor;

        public NextOnGaze readProgress;

        public GameObject readingCanvas;
        public GameObject gameOverCanvas;
        public AudioSource music;

        public Animator[] animators;
        public Slider distractionSlider;
        private float distractedTime;
        private bool _isDistracted;

        //lights blinking
        public float maxBrightness = .5f;
        public float blinkingInterval = 1;
        public Light[] lightsToBlink;
        private int direction = -1; // -1 is dimming +1 is lighting

        //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
        public void GazeFocusChanged(bool hasFocus)
        {
            //If this object received focus, fade the object's color to highlight color
            if (hasFocus)
            {
                foreach (Animator anim in animators)
                {
                    anim.speed = 1;
                }
                _isDistracted = false;
            }
            //If this object lost focus, fade the object's color to it's original color
            else
            {
                foreach (Animator anim in animators)
                {
                    anim.speed = 0;
                }
                _isDistracted = true;
            }
        }

        private void Start()
        {
            //_renderer = GetComponent<Renderer>();
            //_originalColor = _renderer.material.color;
            //_targetColor = _originalColor;

            readingCanvas.SetActive(true);
            gameOverCanvas.SetActive(false);
            distractedTime = 0;
        }

        private void Update()
        {
            if (_isDistracted && readProgress.isLookingAtNext == false)
            {
                distractedTime += Time.deltaTime/10;
                music.volume = 0.4f;
            } 
            else
            {
                music.volume = 0.05f;
            }

            distractionSlider.value = distractedTime;
            //Debug.Log(distractedTime);

            //blinking lights
            float addPart = (direction < 0 ? -1 * ((1 / blinkingInterval) * Time.deltaTime *10) : ((1 / blinkingInterval) * Time.deltaTime *10));
            foreach (Light blinkingLight in lightsToBlink)
            {
                float intensity = blinkingLight.intensity + addPart;
                blinkingLight.intensity = intensity;

                if (intensity <= 0 || intensity >= maxBrightness) direction *= -1;
            }
            
            if (distractedTime >= 1 && readProgress.win == false)
            {
                readingCanvas.SetActive(false);
                gameOverCanvas.SetActive(true);
            }


            //This lerp will fade the color of the object
            //if (_renderer.material.HasProperty(_baseColor)) // new rendering pipeline (lightweight, hd, universal...)
            //{
            //    _renderer.material.SetColor(_baseColor, Color.Lerp(_renderer.material.GetColor(_baseColor), _targetColor, Time.deltaTime * (1 / animationTime)));
            //}
            //else // old standard rendering pipline
            //{
            //    _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / animationTime));
            //}
        }
    }
}
