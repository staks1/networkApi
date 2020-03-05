using NetworkApi.Data;
using NetworkApi.Models;
using NetworkApi.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace NetworkApi.Repository
{
    public class LineRepository : ILineRepository
    {
        //privacy for database
        private readonly ApplicationDbContext _db;

        //constructor
        //access the database ->reference to the database
        public LineRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateLine(Line Line)
        {
            _db.Lines.Add(Line);
            return Save();
        }

        public bool DeleteLine(Line Line)
        {
            _db.Lines.Remove(Line);
            return Save();
        }

        public Line GetLine(int LineId)
        {
            return _db.Lines.FirstOrDefault(a => a.Id == LineId);
        }

        public ICollection<Line> GetLines()
        {
            return _db.Lines.OrderBy(a => a.Id).ToList();
        }

        public bool LineExists(int id)
        {
            return _db.Lines.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateLine(Line Line)
        {
            _db.Lines.Update(Line);
            return Save();
        }
    }
}
