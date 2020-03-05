using NetworkApi.Models;
using System.Collections.Generic;

namespace NetworkApi.Repository.IRepository
{
    public interface ILineRepository
    {
        //add methods for accessing the Line repo ,post/get/put/...
        ICollection<Line> GetLines();
        Line GetLine(int LineId);
        //get all the networks  /or one only 
        //check if exists 
        bool LineExists(int id);

        //create ,update ,delete 
        bool CreateLine(Line Line);
        bool UpdateLine(Line Line);
        bool DeleteLine(Line Line);


        //save changes 
        bool Save();
    }
}
