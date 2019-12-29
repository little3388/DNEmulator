# DNEmulator
A stack emulator for the Common Intermediate Language, which follows the ECMA Standards (based on dnlib)


# Usage
Important namespaces:
```C#
  using DNEmulator;
  using DNEmulator.Values;
  using DNEmulator.EmulationResults;
```

Creating a new instance of the emulator:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new Emulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"));   
```

Creating a new instance of emulator with parameter values:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new Emulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"), new Value[] { new StringValue("abc"), new ObjectValue(new int[5]) });   
```

Emulating Method:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var emulator = new Emulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate")); 
  emulator.Emulate();
```

Emulating Instruction:
```C#
  var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
  var method = module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate");
  var emulator = new Emulator(method); 
  foreach(var instruction in method.Body.Instructions)
  {
   var result = emulator.EmulateInstruction(instruction);
  }
```

Accessing Stack:
```C#
  var value = emulator.ValueStack.Pop();
  var values = emulator.ValueStack.Pop(3);
  emulator.ValueStack.Push(new I4Value(0));
```

Events:
```C#
  static void Main(string[] args)
  {
   var module = ModuleDefMD.Load(Assembly.GetEntryAssembly().Location);
   var emulator = new Emulator(module.FindNormal("DNEmulator.Tests.Program").FindMethod("ToEmulate"));
   emulator.BeforeEmulation += BeforeEmulation;
   emulator.AfterEmulation += AfterEmulation;        
  }
  
  private static void BeforeEmulation(Instruction instruction)
  {
    Console.WriteLine("Emulating: " + instruction.ToString());
  }

  private static void AfterEmulation(Instruction instruction)
  {
     Console.WriteLine("Emulated: " + instruction.ToString() + "!");
  }
```

# Values
```
  DNEmulator.Values.I4Value -> int32 value (I4)
  DNEmulator.Values.I8Value -> int64 value (I8)
  DNEmulator.Values.NativeValue -> native int value (I)
  DNEmulator.Values.ObjectValue -> object value (O)
  DNEmulator.Values.StringValue -> string value (S)
  DNEmulator.Values.R8Value -> float(32/64) value (F)
  DNEmulator.Values.UnknownValue -> unknown value
```

# Emulation Results
```
  DNEmulator.EmulationResults.NormalResult -> emulation will continue
  DNEmulator.EmulationResults.JumpResult -> emulator will jump to instruction at given index
  DNEmulator.EmulationResults.ReturnResult -> emulation will end
```

