namespace MODELPriorityQueue.Models
{
    public class Manager : User
    {
        protected override string ServerPath
        {
            get { return "Managers"; }
        }
    }
}
