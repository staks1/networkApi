using NetworkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkApi.Repository.IRepository
{
    public interface INationalNetworkRepository
    {
        //add methods for accessing the nationalNetwork repo ,post/get/put/...
        ICollection<NationalNetwork> GetNationalNetworks();
        NationalNetwork GetNationalNetwork(int nationalNetworkId);
        //get all the networks  /or one only 
        bool NationalNetworkExists(string name);
        //check if exists 
        bool NationalNetworkExists(int id);
      
        //create ,update ,delete 
        bool CreateNationalNetwork(NationalNetwork nationalNetwork);
        bool UpdateNationalNetwork(NationalNetwork nationalNetwork);
        bool DeleteNationalNetwork(NationalNetwork nationalNetwork);


        //save changes 
        bool Save();
    }
}
