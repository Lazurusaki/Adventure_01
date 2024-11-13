using System;

public class Stat<T> : IReadOnlyStat<T> where T : IComparable
{
    private T _value;

    public event Action<T> Changed;

    public Stat()
    {
        Value = default(T);
    }

    public Stat(T value)
    {
        Value = value;
    }

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value))
                return;

            _value = value;
            Changed?.Invoke(_value);
        }
    }
}