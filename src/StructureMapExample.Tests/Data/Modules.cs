using System;

namespace StructureMapExample.Tests.Data
{
    public interface IModule { }

    public interface IModuleA { }

    public interface IModuleB { }

    public interface IModuleC { }

    public class ModuleA : IModule, IModuleA { }

    public class ModuleB : IModule, IModuleB { }

    public class ModuleC : IModule, IModuleC { }

    public class DisposableModule : IModule, IDisposable
    {
        public void Dispose()
        {
            IsDisposed = true;
        }

        public bool IsDisposed { get; set; }
    }
}