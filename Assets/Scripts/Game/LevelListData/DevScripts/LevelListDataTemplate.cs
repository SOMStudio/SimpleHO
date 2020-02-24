using UnityEngine;

namespace Game.LevelListData.DevScripts
{
    [CreateAssetMenu(fileName = "New LevelListDataTemplate", menuName = "SOMStudio/Data/Create Level List Data Template")]
    public class LevelListDataTemplate : ScriptableObject
    {
        [SerializeField] private LevelData[] data;

        public LevelData[] Data => data;
    }
}
