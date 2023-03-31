using System.Collections.Generic;
using UnityEngine;

namespace MonsterExterminator.Common.BehaviorTree
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> blackboardData = new();

        public delegate void OnBlackboardValueChangeDelegate(string key, object value);

        public event OnBlackboardValueChangeDelegate OnBlackboardValueChange;

        public void SetOrAddData(string key, object value)
        {
            blackboardData[key] = value;
            OnBlackboardValueChange?.Invoke(key, value);
            Debug.Log(OnBlackboardValueChange?.GetInvocationList().Length);
        }

        public bool GetBlackboardData<T>(string key, out T value)
        {
            value = default;
            if (blackboardData.ContainsKey(key))
            {
                value = (T)blackboardData[key];
                return true;
            }

            return false;
        }

        public bool HasKey(string key) => blackboardData.ContainsKey(key);

        public void RemoveBlackboardData(string key)
        {
            blackboardData.Remove(key);
            OnBlackboardValueChange?.Invoke(key, null);
            Debug.Log(OnBlackboardValueChange?.GetInvocationList().Length);
        }
    }
}