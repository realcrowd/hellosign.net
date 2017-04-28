using System;
using System.IO;

namespace RealCrowd.HelloSign.Models
{
    [Serializable]
    public class FileResponse : IDisposable
    {
        private bool disposed;

        public Stream Stream { get; internal set; }

        public string FileName { get; internal set; }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }
            if (disposing && this.Stream != null)
            {
                this.Stream.Dispose();
            }
            this.disposed = true;
        }
    }
}
