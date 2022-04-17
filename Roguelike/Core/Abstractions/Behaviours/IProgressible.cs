using Roguelike.Properties;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IProgressible
{
    ProgressionProperties Progression { get; set; }
}