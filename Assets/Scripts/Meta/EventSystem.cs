using System;
using System.Collections.Generic;
using System.Linq;

public static class EventSystem
{
    private static Dictionary<ContentEventType, Delegate> _eventListeners = new Dictionary<ContentEventType, Delegate>();
    private static Dictionary<ContentEventType, Delegate> _eventListenersWithParamater = new Dictionary<ContentEventType, Delegate>();

    public static void AddEventListener(ContentEventType eventType, Action callBack)
    {
        if (_eventListeners.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            _eventListeners[eventType] = Delegate.Combine(mergedCallBacks, callBack);
        }
        else
        {
            _eventListeners[eventType] = callBack;
        }
    }

    public static void AddEventListener<T>(ContentEventType eventType, Action<T> callBack)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            _eventListenersWithParamater[eventType] = Delegate.Combine(mergedCallBacks, callBack);
        }
        else
        {
            _eventListenersWithParamater[eventType] = callBack;
        }
    }

    public static void RemoveEventListener(ContentEventType eventType, Action callBack)
    {
        if (_eventListeners.TryGetValue(eventType, out Delegate mergedCallBacks))
        {
            mergedCallBacks = Delegate.Remove(mergedCallBacks, callBack);
            if (mergedCallBacks == null)
            {
                _eventListeners.Remove(eventType);
            }
            else
            {
                _eventListeners[eventType] = mergedCallBacks;
            }
        }
    }

    public static void RemoveEventListener<T>(ContentEventType eventType, Action<T> callBack)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out Delegate mergedCallBacks))
        { 
            mergedCallBacks = Delegate.Remove(mergedCallBacks, callBack);
            if (mergedCallBacks == null)
            {
                _eventListenersWithParamater.Remove(eventType);
            }
            else
            {
                _eventListenersWithParamater[eventType] = mergedCallBacks;
            }
        }
    }

    public static void Broadcast(ContentEventType eventType)
    {
        if (_eventListeners.TryGetValue(eventType, out var mergedCallBacks))
        {
            Action[] actionList = mergedCallBacks.GetInvocationList().Cast<Action>().ToArray();
            foreach(Action action in actionList)
            {
                action.Invoke();
            }
        }
    }

    public static void Broadcast<T>(ContentEventType eventType, T arg)
    {
        if (_eventListenersWithParamater.TryGetValue(eventType, out var mergedCallBacks))
        {
            Action<T>[] actionList = mergedCallBacks.GetInvocationList().Cast<Action<T>>().ToArray();
            foreach (Action<T> action in actionList)
            {
                action.Invoke(arg);
            }
        }
    }
}
