// IInteractable.cs
using UnityEngine;

/// <summary>
/// Contract for any object the player can interact with.
/// Attach this to scripts like QuestObject, Door, ItemPickup, etc.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Called when the player interacts with this object.
    /// </summary>
    void Interact();
    bool IsAvailable();
}
