using NetworkApi.Data;
using NetworkApi.Models;
using NetworkApi.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace NetworkApi.Repository
{
    public class NationalNetworkRepository : INationalNetworkRepository
    {
        //privacy for database
        private readonly ApplicationDbContext _db;

        //constructor
        //access the database ->reference to the database
        public NationalNetworkRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateNationalNetwork(NationalNetwork nationalNetwork)
        {
            _db.NationalNetworks.Add(nationalNetwork);
            return Save();
        }

        public bool DeleteNationalNetwork(NationalNetwork nationalNetwork)
        {
            _db.NationalNetworks.Remove(nationalNetwork);
            return Save();
        }

        public NationalNetwork GetNationalNetwork(int nationalNetworkId)
        {
            return _db.NationalNetworks.FirstOrDefault(a => a.Id == nationalNetworkId);
        }

        public ICollection<NationalNetwork> GetNationalNetworks()
        {
            return _db.NationalNetworks.OrderBy(a => a.Name).ToList();
        }

        public bool NationalNetworkExists(string name)
        {
            bool value = _db.NationalNetworks.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalNetworkExists(int id)
        {
            return _db.NationalNetworks.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalNetwork(NationalNetwork nationalNetwork)
        {
            _db.NationalNetworks.Update(nationalNetwork);
            return Save();
        }
    }
}
