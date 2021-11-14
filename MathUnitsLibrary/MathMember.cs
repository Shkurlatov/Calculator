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
            Priority = (priorityLayer * _amountOperationTypes) + OperationPriority(operation);
        }

        public MathMember(double value, MathOperation operation)
        {
            Value = value;
            Operation = operation;
            Priority = OperationPriority(operation);
        }

        private int OperationPriority(MathOperation operation)
        {
            if (operation > MathOperation.Multiplication)
            {
                return 2;
            }

            if (operation > MathOperation.Addition)
            {
                return 1;
            }

            return 0;
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
