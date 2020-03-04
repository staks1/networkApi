using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetworkApi.Data;
using NetworkApi.Models;
using NetworkApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        //we need the settings secret key
        private readonly AppSettings _appSettings; 

        public UserRepository(ApplicationDbContext db,IOptions<AppSettings> appsettings)
        {
            _db = db;
            //retrieve the secret key value
            _appSettings = appsettings.Value;
        }



        public User Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            //if user is found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //add claim based on user id 
                //maybe add more claims later 
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name ,user.Id.ToString())

            }),
                //expires in 7 days time
                Expires = DateTime.UtcNow.AddDays(7),
                //use hashmap sha256 algorithm for encryption
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //pass the descriptor to generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            //hide password
            user.Password = "";
            return user;

        }

        public bool isUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
