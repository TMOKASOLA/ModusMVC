using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModusMVC.Models
{
    public class Authentication
    {
        public bool Authenticated(string _username, string _password)
        {
            ModusDNAEntities _db = new ModusDNAEntities();

          

            bool Authenticcation = false;

            var result = (from x in _db.Admins where x.AdminPassword == _password && x.AdminUsername == _username select x).Count();

            if (result > 0)
            {
                Authenticcation = true;
            }
            return Authenticcation;
        }
    }
    
  
}