namespace PROG3A_POE.Models
{
  
    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string User_Roles { get; set; }

        public User(string name, string username, string password, string user_Roles)
        {
            Name = name;
            Username = username;
            Password = password;
            User_Roles = user_Roles;
        }

        public User()
        {

        }

        public bool checkUser(string user, string pass, User filteredKeys)
        {

            if (filteredKeys.Username.Equals(user) && filteredKeys.Password.Equals(pass))
            {
                return true;
            }
            else { return false; }


        }

        

    
}
}
