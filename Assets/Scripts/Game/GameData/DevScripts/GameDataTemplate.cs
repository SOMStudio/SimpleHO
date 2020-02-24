using UnityEngine;

namespace Game.GameData.DevScripts
{
    [CreateAssetMenu(fileName = "New GameDataTemplate", menuName = "SOMStudio/Data/Create Game Data Template")]
    public class GameDataTemplate : ScriptableObject
    {
        [SerializeField] private GameData data;

        public GameData Data => data;
    }
}
