// Script from This is GameDev

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new();

    // Raise event through different methods signatures
    
    public void Raise(Component sender, object data)
    {
        foreach (GameEventListener listener in listeners)
            listener.OnEventRaised(sender, data);
    }

    // Manage Listeners 
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
