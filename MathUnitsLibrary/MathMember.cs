using System;

namespace MathUnitsLibrary
{
    public class MathMember
    {
        public double Value;
        public MathOperation Operation { get; }
        public int Priority { get; }
        public bool IsNegative { get; private set; }

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


        public void MakeNegative()
        {
            IsNegative = true;
        }

        public override bool Equals(object obj)
        {
            return obj is MathMember member &&
                   Value == member.Value &&
                   Operation == member.Operation &&
                   Priority == member.Priority &&
                   IsNegative == member.IsNegative;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Operation, Priority, IsNegative);
        }
    }
}
