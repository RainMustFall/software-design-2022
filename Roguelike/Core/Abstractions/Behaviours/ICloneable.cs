namespace Roguelike.Core.Abstractions.Behaviours;

public interface ICloneable<out TBase>
{
    TBase Clone();
}