using Cinema.DAL;

namespace Cinema.BLL
{
    public class DiagnosticsService
    {
        private readonly Db _db;

        public DiagnosticsService()
        {
            _db = new Db();
        }

        public bool TryTestConnection(out string message)
        {
            return _db.TestConnection(out message);
        }
    }
}
