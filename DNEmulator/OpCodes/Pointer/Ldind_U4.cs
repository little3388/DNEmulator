﻿using DNEmulator.Abstractions;
using DNEmulator.EmulationResults;
using DNEmulator.Exceptions;
using DNEmulator.Values;
using dnlib.DotNet.Emit;

namespace DNEmulator.OpCodes.Pointer
{
    public class Ldind_U4 : IOpCode
    {
        public Code Code => Code.Ldind_U4;

        public unsafe EmulationResult Emulate(Context ctx)
        {
            if (!(ctx.Stack.Pop() is NativeValue nativeValue))
                throw new InvalidILException(ctx.Instruction.ToString());

            ctx.Stack.Push(new I4Value((int)*(uint*)nativeValue.Value));

            return new NormalResult();
        }
    }
}