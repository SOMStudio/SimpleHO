using UnityEngine.Events;

namespace Base.Resource.Interfaces
{
    interface IResource<T> where T : struct
    {
    void Set(T value);
    T Get();

    void Add(T value);
    void Reduce(T value);

    void AddListener(UnityAction<T> listener);
    void RemoveListener(UnityAction<T> listener);
    }
}
