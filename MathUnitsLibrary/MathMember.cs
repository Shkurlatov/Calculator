﻿using System;

namespace MathUnitsLibrary
{
    public class MathMember
    {
        public decimal Value;
        public MathOperation Operation { get; }
        public int Priority { get; }

        private const int _amountOperationTypes = 2;

        public MathMember(decimal value, MathOperation operation, int priorityLayer)
        {
            Value = value;
            Operation = operation;
            Priority = (priorityLayer * _amountOperationTypes) + OperationPriority(operation);
        }

        public MathMember(decimal value, MathOperation operation)
        {
            Value = value;
            Operation = operation;
            Priority = OperationPriority(operation);
        }

        private int OperationPriority(MathOperation operation)
        {
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
