using System;

[AttributeUsage(AttributeTargets.Field)]
public class PriorityAttribute : Attribute
{
    public int Priority { get; private set; }

    public PriorityAttribute(int priority)
    {
        Priority = priority;
    }
}
