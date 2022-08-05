using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Root", menuName = "Configs/Root")]
    public class RootConfig : ScriptableObject
    {
        public SceneConfig sceneConfig;
    }
}