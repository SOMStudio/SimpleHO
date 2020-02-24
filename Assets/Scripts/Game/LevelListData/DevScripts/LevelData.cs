using System;
using System.Text;
using Base.TimeControl;
using UnityEngine;

namespace Game.LevelListData.DevScripts
{
    [Serializable]
    public class LevelData
    {
        [Header("Name scene")]
        [SerializeField] private string nameScene;

        [Header("Type level")]
        [SerializeField] private TypeLevel typeLevel;

        [Header("Time level")]
        [SerializeField] private bool timeLimit;
        [SerializeField] private float timeForLimit;
        
        [Header("Life player")]
        [SerializeField] private int lifeLevel;

        [Header("Items name")]
        [SerializeField] private string[] itemsName;

        public string NameScene => nameScene;
        public TypeLevel TypeLevel => typeLevel;
        public bool TimeLimit => timeLimit;
        public float TimeForLimit => timeForLimit;
        public int LifeLevel => lifeLevel;
        public string[] ItemsName => itemsName;
        
        private StringBuilder _stringBuilder = new StringBuilder();
        
        public string GetStringTask()
        {
            _stringBuilder.Clear().Append("<b>Task:</b><n>");

            _stringBuilder.Append("You must find all items");

            switch (TypeLevel)
            {
                case TypeLevel.NightHO:
                    _stringBuilder.Append(" in night state");
                    break;
            }

            if (TimeLimit)
                _stringBuilder.AppendFormat(", with limit time (limit: {0})", TimeHelp.GetFormattedTime(timeForLimit, "MM:SS"));
            else
                _stringBuilder.Append(", without limit time");
            
            _stringBuilder.Append(".");
            
            return _stringBuilder.ToString();
        }
    }
}
