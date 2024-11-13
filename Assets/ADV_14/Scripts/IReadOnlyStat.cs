using System;

public interface IReadOnlyStat<T>
{
    T Value { get; }
    
    event Action<T> Changed;
}
