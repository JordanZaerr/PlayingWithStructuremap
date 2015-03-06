namespace StructureMapExample.Old.Tests.Data
{
    public interface IModule { }

    public interface IModuleA { }

    public interface IModuleB { }

    public interface IModuleC { }

    public class ModuleA : IModule, IModuleA { }

    public class ModuleB : IModule, IModuleB { }

    public class ModuleC : IModule, IModuleC { }
}