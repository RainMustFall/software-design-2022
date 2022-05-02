using Roguelike.Helpers;

namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// Main interface that contains methods and fields that are applicable to every playable object, such as PlayerCharacter,
/// NPC, enemy, various projectiles (arrows, magic) and so on. 
/// </summary>
public interface IPlayable
{
    /// <summary>
    /// Id that allows to distinguish different playable instances. 
    /// </summary>
    public int Id => IdGenerator.GenerateId();
}