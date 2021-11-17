using System;

namespace MathUnitsLibrary
{
    public class MathMember
    {
        public double Value;
        public MathOperation Operation { get; }
        public int Priority { get; }

        private const int _amountOperationTypes = 3;

        public MathMember(double value, MathOperation operation, int priorityLayer)
        {
            Value = value;
            Operation = operation;
            Priority = (priorityLayer * _amountOperationTypes) + OperationAttributes.Get<PriorityAttribute>(operation).Priority;
        }

        public MathMember(double value, MathOperation operation)
        {
            Value = value;
            Operation = operation;
            Priority = OperationAttributes.Get<PriorityAttribute>(operation).Priority;
        }

        public override bool Equals(object obj)
        {
            return obj is MathMember member &&
                   Value == member.Value &&
                   Operation == member.Operation &&
                   Priority == member.Priority;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Operation, Priority);
        }
    }
}
