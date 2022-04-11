using Roguelike.Core.Models;

namespace Roguelike.Core.Abstractions.Behaviours;

public interface IProgressible
{
    ProgressionProperties Progression { get; set; }
}