using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "UnitsConfig", menuName = "Configs/Units")]
    public class UnitsConfig : ScriptableObject
    {
        public CharacterController controllerPrefab;
    }
}