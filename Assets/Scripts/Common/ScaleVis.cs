﻿using System;
using UnityEngine;

namespace Utils {
    public class ScaleVis : MonoBehaviour, IVisibilityControl {

        [SerializeField] private bool animateOnStart = true;
        [SerializeField] private bool visibleOnStart = true;
        [SerializeField] private float speed = .2f;
        
        public bool IsVisible { get; private set; }

        public Action<bool> OnFinishedAnimation;

        private float current;
        private float target = 1;
        private float targetRef;
        
        private void Start() {
            if (animateOnStart) {
                Scale(0);
            }
            if (!IsVisible) {
                Visible(visibleOnStart, !animateOnStart);
            }
        }

        public void Visible(bool show, bool instant = false) {
            var target = show ? 1 : 0;
            Visible(target, instant);
        }

        public void Visible(float target, bool instant = false) {
            enabled = true;
            IsVisible = target == 1;

            if (float.IsNaN(target)) {
                target = 0;
            }
            
            this.target = target;
            
            if (instant) {
                current = target;
            }
        }

        public void Show(bool instant = false) {
            Visible(true, instant);
        }

        public void Hide(bool instant = false) {
            Visible(false, instant);
        }

        public void Toggle(bool instant = false) {
            Visible(!IsVisible, instant);
        }

        public void Update() {
            if (Mathf.Abs(current - target) < .0001f) {
                if (OnFinishedAnimation != null)
                    OnFinishedAnimation(IsVisible);
                enabled = false;
            }

            current = Mathf.SmoothDamp(current, target, ref targetRef, speed);
            Scale(current);
        }

        private void Scale(float amount) {
            transform.localScale = Vector3.one * amount;
        }
    }
}