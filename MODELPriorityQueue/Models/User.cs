namespace MODELPriorityQueue.Models
{
<<<<<<< HEAD
    public abstract class User : DatabaseEntity<User>
=======
    public abstract class User<T> : DatabaseEntry<T> where T : User<T>
>>>>>>> d740321f29e99d3cf4abb813c3df9a3d3c8a6e12
    {
        public string username;
        public string password;
        public string firstName;
        public string lastName;

        public string Username
        {
            get { return username; }
            set { Set(() => Username, ref username, value); }
        }

        public string Password
        {
            get { return password; }
            set { Set(() => Password, ref password, value); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { Set(() => FirstName, ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { Set(() => LastName, ref lastName, value); }
        }
    }
}
