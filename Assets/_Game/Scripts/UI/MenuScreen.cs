using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuScreen : UIScreen
    {
        [BoxGroup("MenuButtons")]
        [SerializeField] private Button trainingButton;
        private void Start()
        {
            OpenScreen();
        }
    }
}