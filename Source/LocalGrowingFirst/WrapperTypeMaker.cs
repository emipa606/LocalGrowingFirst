// FAILED BUT I AM KEEPING BECAUSE I AM A WHORE ... I MEAN A HOARDER


using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using RimWorld;
using Verse.AI;

namespace LocalGrowingFirst
{
    static public class WrapperTypeMaker
    {
		static private ModuleBuilder moduleBuilder;
		static public readonly AssemblyName assemblyName = new AssemblyName("WorkGiver_LocalZone_Wrappers");
        
        static public Type CreateWrappedScanner(Type typeToWrap)
		{
			TypeBuilder tb = GetTypeBuilder(typeToWrap);
			CreateConstructor(tb, typeToWrap);
			return tb.CreateType();
		}

		static private ModuleBuilder GetModuleBuilder()
		{
			if(moduleBuilder == null) {
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain
						.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
				moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
			}
			return moduleBuilder;
		}

		static private TypeBuilder GetTypeBuilder(Type typeToWrap)
		{
			string typeSignature = typeToWrap.Name + "_Wrapped";
			TypeBuilder tb = GetModuleBuilder().DefineType(typeSignature
						, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass
							| TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit
							| TypeAttributes.AutoLayout, typeof(WorkGiver_LocalZoneWrapper));
			return tb;  
		}

		static private void CreateConstructor(TypeBuilder tb, Type wrappedScannerType)
		{
			FieldInfo wrappedScannerField = typeof(WorkGiver_LocalZoneWrapper).GetField(nameof(WorkGiver_LocalZoneWrapper.wrappedScanner)
											, BindingFlags.Public | BindingFlags.Instance);
			MethodInfo hitting = typeof(WrapperTypeMaker).GetMethod("Hitting");

			Log.Message($"Test:{wrappedScannerType == null}   Test2:{wrappedScannerField == null}   Test3:{typeof(WorkGiver_Scanner).IsAssignableFrom(wrappedScannerType)}");
        
			ConstructorBuilder builder = tb.DefineConstructor(MethodAttributes.Public,
                                                    CallingConventions.Standard, new Type[0]);
			ILGenerator constructorIL = builder.GetILGenerator();
			//constructorIL.Emit(OpCodes.Ldarg_0);
			//constructorIL.Emit(OpCodes.Newobj, wrappedScannerType);
			//constructorIL.Emit(OpCodes.Stfld, wrappedScannerField);
			constructorIL.Emit(OpCodes.Ldarg_0);
			constructorIL.Emit(OpCodes.Call, hitting);
		}

		static public void Hitting(WorkGiver_LocalZoneWrapper wg)
		{
			Log.Message("Hitting");
			Log.Message($" wg == null:{wg == null}   wg.wrappedScanner == null:{wg.wrappedScanner == null}");
		}
    }
}
