using System;
using Base.Resource.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Resource
{
    [Serializable]
    public class IntResource : IResource<int>
    {
        [SerializeField] private int value;
        [Header("Events")]
        [SerializeField] private IntEvent changeEvent;

        public void Set(int newValue)
        {
            value = newValue;
        }

        public int Get()
        {
            return value;
        }

        public void Add(int newValue)
        {
            value += newValue;

            changeEvent?.Invoke(value);
        }

        public void Reduce(int newValue)
        {
            if (value > 0)
            {
                value -= newValue;
                if (value < 0)
                {
                    value = 0;
                }

                changeEvent?.Invoke(value);
            }
        }

        public void Change(int newValue)
        {
            value = newValue;

            changeEvent?.Invoke(value);
        }

        public void AddListener(UnityAction<int> listener)
        {
            changeEvent.AddListener(listener);
        }

        public void RemoveListener(UnityAction<int> listener)
        {
            changeEvent.RemoveListener(listener);
        }
    }

    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
    }
}
