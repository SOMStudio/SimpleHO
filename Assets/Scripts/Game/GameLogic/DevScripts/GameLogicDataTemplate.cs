using UnityEngine;

namespace Game.GameLogic.DevScripts
{
    [CreateAssetMenu(fileName = "New GameLogicDataTemplate", menuName = "SOMStudio/Data/Create Game logic Data Template")]
    public class GameLogicDataTemplate : ScriptableObject
    {
        [SerializeField] private GameLogicData data;

        public GameLogicData Data => data;
    }
}
