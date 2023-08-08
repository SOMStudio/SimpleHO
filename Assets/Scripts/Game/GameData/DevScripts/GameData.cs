using System;
using UnityEngine;

namespace Game.GameData.DevScripts
{
    [Serializable]
    public class GameData
    {
        [Header("Game")]
        [SerializeField] private float delayForWinMessage = 1.0f;

        [Header("Level")]
        [SerializeField] private int activeLevel = -1;
        [SerializeField] private int bonusForItem = 10;

        [Header("Sound&Music")]
        [SerializeField] private float defaultVolume = 0.5f;

        public float DelayForWinMessage => delayForWinMessage;

        public int ActiveLevel
        {
            get => activeLevel;
            set => activeLevel = value;
        }

        public int BonusForItem => bonusForItem;

        public float DefaultVolume => defaultVolume;
    }
}
