﻿using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Enumerations;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Conditionals
{
    public class Clt : IOpCode
    {
        public Code Code => Code.Clt;

        public EmulationResult Emulate(Context ctx)
        {
            var secondValue = ctx.Stack.Pop();
            var firstValue = ctx.Stack.Pop();

            bool statement;
            switch (firstValue.ValueType)
            {
                case DNValueType.Int32:
                    statement = ((I4Value)firstValue).Value < ((I4Value)secondValue).Value;
                    break;
                case DNValueType.Int64:
                    statement = ((I8Value)firstValue).Value < ((I8Value)secondValue).Value;
                    break;
                case DNValueType.Real:
                    statement = ((R8Value)firstValue).Value < ((R8Value)secondValue).Value;
                    break;
                default:
                    throw new InvalidILException(ctx.Instruction.ToString());

            }
            ctx.Stack.Push((statement) ? new I4Value(1) : new I4Value(0));
            return new NormalResult();
        }
    }
}