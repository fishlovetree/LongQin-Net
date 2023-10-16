using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service
{
    public class ServiceBase : IDisposable
    {
        public IList<IDisposable> DisposableObjects { get; private set; }

        public ServiceBase() {
            this.DisposableObjects = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj) {
            var disposable = obj as IDisposable;
            if(disposable != null) {
                this.DisposableObjects.Add(disposable);
            }
        }

        public void Dispose() {
            foreach(IDisposable obj in this.DisposableObjects) {
                if(obj != null) {
                    obj.Dispose();
                }
            }
        }
    }
}
