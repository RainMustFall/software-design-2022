using Roguelike.Properties;

namespace Roguelike.Core.Abstractions.Behaviours;

/// <summary>
/// Contains fields that are applicable to every progressible character (that is able to level up and gain experience
/// in some cases).
/// </summary>
public interface IProgressible
{
    ProgressionProperties Progression { get; set; }
}