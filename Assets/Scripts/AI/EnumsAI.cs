using UnityEngine;

namespace AI
{
    public enum NodeResult
    {
        Success,
        Failure,
        Inprogress
    }

    /// <summary>
    /// RunCondition - Условие выполнения
    /// </summary>
    public enum RunCondition                                                // Условие выполнения
    {
        [Tooltip("Ключ существует")] KeyExists,                             // Ключ существует
        [Tooltip("Ключ не существует")] KeyNotExists                        // Ключ не существует
    }

    /// <summary>
    ///  NotifyRule - Правило Уведомления
    /// </summary>
    public enum NotifyRule                                                  // Правило Уведомления
    {
        [Tooltip("Изменение условий запуска")] RunConditionChange,          // Изменение условий запуска
        [Tooltip("Изменение значения ключа")] KeyValueChange                // Изменение значения ключа
    }

    /// <summary>
    /// NotifyAbort - При прерывании Уведомить 
    /// </summary>
    public enum NotifyAbort                                                 // Уведомить о прерывании
    {
        none,
        [Tooltip("себя")] self,                                             // себя
        [Tooltip("ниже")] lower,                                            // ниже
        [Tooltip("оба")] both                                               // оба
    }

}