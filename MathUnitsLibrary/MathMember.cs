using System;

namespace MathUnitsLibrary
{
    public class MathMember
    {
        public decimal Value;
        public MathOperation Operation { get; }
        public int PriorityLayer { get; }

        public MathMember(decimal value, MathOperation operation, int priorityLayer)
        {
            Value = value;
            Operation = operation;
            PriorityLayer = priorityLayer;
        }

        public MathMember(decimal value, MathOperation operation)
        {
            Value = value;
            Operation = operation;
        }

        public override bool Equals(object obj)
        {
            return obj is MathMember member &&
                   Value == member.Value &&
                   Operation == member.Operation &&
                   PriorityLayer == member.PriorityLayer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Operation, PriorityLayer);
        }
    }
}
