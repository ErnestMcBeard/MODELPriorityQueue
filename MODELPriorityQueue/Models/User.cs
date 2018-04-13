namespace MODELPriorityQueue.Models
{
    public abstract class User<T> : DatabaseEntity<T> where T : User<T>
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
