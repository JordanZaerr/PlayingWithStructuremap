using System;

namespace StructureMapExample.Tests.Data
{
    public interface IService { }

    public class Service : IService { }

    public class InMemoryService : IService { }

    public class DisposableService : IService, IDisposable{
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}