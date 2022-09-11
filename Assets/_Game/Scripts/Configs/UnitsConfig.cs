using System;
using Game.Units;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "UnitsConfig", menuName = "Configs/Units")]
    public class UnitsConfig : ScriptableObject
    {
        public CharacterController controllerPrefab;

        public UnitView[] unitViews;

    }
}