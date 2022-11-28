using UniRx;
using UnityEngine;

namespace Game.Units
{
    public class AnimatorObserver : MonoBehaviour
    {
        public ReactiveCommand OnAnimationEnd { get; } = new ReactiveCommand();
        public bool IsAnimationPlay;

        public void StartAnimationObserve() => IsAnimationPlay = true;

        public void StopAnimationObserve()
        {
            
            IsAnimationPlay = false;
            OnAnimationEnd.Execute();
        }
    }
}