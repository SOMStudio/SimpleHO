﻿using System;
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
        [SerializeField] private int bunusForItem = 10;

        [Header("Sound&Misuc")]
        [SerializeField] private float defaulValume = 0.5f;

        public float DelayelayForWinMessage => delayForWinMessage;

        public int ActiveLevel {
            get { return activeLevel; }
            set { activeLevel = value; }
        }

        public int BonusForItem => bunusForItem;

        public float DefaultValume => defaulValume;
    }
}
