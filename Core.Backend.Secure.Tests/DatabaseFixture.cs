namespace Core.Backend.Secure.Tests
{
    public class DatabaseFixture<T> : IDisposable
    {
        private static readonly object Lock = new();
        private static bool _databaseInitialized;

        private readonly WebAppFactory<Program> _factory;

        public DatabaseFixture(bool useMigrationsForDatabaseCreation = false)
        {
            // Use it for CQS and IMapper configuration as for secret values over web project (e.g. Z.EntityFramework.Extensions license).
            _factory = new WebAppFactory<Program>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposing) return;
            _factory.Dispose();
        }
    }
}
