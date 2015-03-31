using System;

namespace StructureMapExample.Tests.Data
{
    public interface IService
    {
        void DoSomething(string someArgument, int anotherArgument);
    }

    public class Service : IService 
    {
        public void DoSomething(string someArgument, int anotherArgument){}
    }

    public class DisposableService : IService, IDisposable{
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            IsDisposed = true;
        }

        public void DoSomething(string someArgument, int anotherArgument){}
    }
}