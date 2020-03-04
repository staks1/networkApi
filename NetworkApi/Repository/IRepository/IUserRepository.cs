using NetworkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkApi.Repository.IRepository
{
   public interface IUserRepository 
    {
        //check is user is qunique method
        bool isUniqueUser(string username);
        //authenticate the user
        User Authenticate(string username, string password);
        //register user 
        User Register(string username, string password);
    }
}
