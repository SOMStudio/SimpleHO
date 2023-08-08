using System;
using Base.Resource.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Resource
{
    [Serializable]
    public class FloatResource : IResource<float>
    {
        [SerializeField] private float value;
        [Header("Events")]
        [SerializeField] private FloatEvent changeEvent;

        public void Set(float newValue)
        {
            value = newValue;
        }

        public float Get()
        {
            return value;
        }

        public void Add(float newValue)
        {
            value += newValue;

            changeEvent?.Invoke(value);
        }

        public void Reduce(float newValue)
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

        public void Change(float newValue)
        {
            value = newValue;

            changeEvent?.Invoke(value);
        }

        public void AddListener(UnityAction<float> listener)
        {
            changeEvent.AddListener(listener);
        }

        public void RemoveListener(UnityAction<float> listener)
        {
            changeEvent.RemoveListener(listener);
        }

        [Serializable]
        public class FloatEvent : UnityEvent<float>
        {
        }
    }
}
