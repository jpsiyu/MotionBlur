using System.Collections;
using System.Collections.Generic;
using System;

public class EventManager  {
    public static readonly EventManager Instance = new EventManager();
    private EventManager() { }

    public delegate void EventDelegate<T>(T e) where T : EventArgs;
    private Dictionary<Type, Delegate> _typeDelegateDict = new Dictionary<Type, Delegate>();

    public void AddListener<T>(EventDelegate<T> listene) where T : EventArgs {
        Delegate d;
        if (_typeDelegateDict.TryGetValue(typeof(T), out d)) {
            _typeDelegateDict[typeof(T)] = Delegate.Combine(d, listene);
        }else {
            _typeDelegateDict[typeof(T)] = listene;
        }
    }

    public void RemoveListener<T>(EventDelegate<T> listener) where T: EventArgs {
        Delegate d;
        if (_typeDelegateDict.TryGetValue(typeof(T), out d)){
            Delegate curDel = Delegate.Remove(d, listener);
            if (curDel == null)
                _typeDelegateDict.Remove(typeof(T));
            else
                _typeDelegateDict[typeof(T)] = curDel;
        }
    }

    public void Send<T>(T e) where T : EventArgs {
        if (e == null)
            throw new ArgumentNullException("e");
        Delegate d;
        if (_typeDelegateDict.TryGetValue(typeof(T), out d)){
            EventDelegate<T> callback = d as EventDelegate<T>;
            if (callback != null)
                callback(e);
        }
    }
    
}
