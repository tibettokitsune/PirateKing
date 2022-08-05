using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Scene config", menuName = "Configs/Scenes")]
    public class SceneConfig : ScriptableObject
    {
        public string[] environmentScenes;

        public string singlePlayerScene;

        public bool isOverrideEnvScene;

        public string overrideSceneName;
    }
}