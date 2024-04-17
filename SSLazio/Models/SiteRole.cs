using System;
using System.Linq;
using System.Web.Security;

namespace SSLazio.Models
{
    public class SiteRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        // Qui controllo il ruolo dell'utente registrato
        public override string[] GetRolesForUser(string Email)
        {
            using (var context = new ModelDbContext())
            {
                var user = context.Utenti.FirstOrDefault(e => e.Email == Email);
                if (user == null)
                    return new string[] { };

                if (string.Equals(user.Ruolo.Trim(), "Admin", StringComparison.OrdinalIgnoreCase))
                    return new string[] { "Admin" };
                else if (string.Equals(user.Ruolo.Trim(), "Tifoso", StringComparison.OrdinalIgnoreCase))
                    return new string[] { "Tifoso" };
                else
                    return new string[] { "User" };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}