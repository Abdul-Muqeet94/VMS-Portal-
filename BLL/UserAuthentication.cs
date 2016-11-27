using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DLL.Model;
using WebMatrix.WebData;
using System.Collections.Specialized;

using System.Data.Entity;
using System.Web;
using System.Web.Caching;


namespace BLL
{
    public class VMSMembershipProvider : MembershipProvider
    {
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            // check if the old password id correct
            if (!ValidateUser(username, oldPassword))
                return false;

            using (var db = new Context())
            {
                // get the employee against the user name
                Users emp = db.Users.Where(m => m.name.Equals(username) && m.active).FirstOrDefault();

                // return flase if the emp does not exist.
                if (emp == null)
                    return false;

                // change the password
                DLL.Commons.Passwords.setPassword(emp, newPassword);


                // commit to db.
                db.SaveChanges();

                return true;
            }
        }

        public override bool ValidateUser(string username, string attemptedPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(attemptedPassword))
                return false;

            using (var db = new Context())
            {

                var user = db.Users.Where(m => m.name.Equals(username) && m.active).FirstOrDefault();

                if (user == null)
                    return false;

                return DLL.Commons.Passwords.validate(user, attemptedPassword);
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = new Context())
            {
                Users emp = db.Users.Where(m => m.name.Equals(username) && m.active).FirstOrDefault();

                if (emp == null)
                    return null;


                return new MembershipUser(this.Name,
                                            username,
                                            "",
                                            emp.name,
                                            null,
                                            null,
                                            false,
                                            false,
                                            DateTime.Now,
                                            DateTime.Now,
                                            DateTime.Now,
                                            DateTime.Now,
                                            DateTime.Now);
            }
        }

        #region Unused

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        #endregion


    }


    public class VMSRoleProvider : RoleProvider
    {
        #region Properties

        private int _cacheTimeoutInMinutes = 30;

        #endregion

        #region Overrides of RoleProvider


        /// Initialize values from web.config.

        /// name: the friendly name of the provider
        /// config: A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Call base method
            base.Initialize(name, config);
        }


        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.


        /// returns: true if the specified user is in the specified role for the configured applicationName; otherwise, false.

        /// name: The user name to search for.
        /// roleName: The role to search in.
        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }


        /// Gets a list of the roles that a specified user is in for the configured applicationName.


        /// returns: A string array containing the names of all the roles that the specified user is in for the configured applicationName.

        /// username: The user to return a list of roles for.
        public override string[] GetRolesForUser(string username)
        {
            //Return if the user is not authenticated
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            //Return if present in Cache
            /*var cacheKey = string.Format("UserRoles_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (string[])HttpRuntime.Cache[cacheKey];*/


            //Get the roles from DB
            var userRoles = new string[] { };
            using (var db = new Context())
            {
                var user = db.Users.Where(m => m.name.Equals(username) && m.active).FirstOrDefault();

                if (user != null)
                    userRoles = new[] { user.accessGroup.name };
            }

            //Store in cache
            //HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

            // Return
            return userRoles.ToArray();
        }

        #endregion

        #region Overrides of RoleProvider that throw NotImplementedException

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}
