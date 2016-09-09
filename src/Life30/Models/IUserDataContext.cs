namespace Life30.Models
{
    public interface IUserDataContext
    {

        void AddNewUser(string name);
        int GetUserIdWhithName(string name);
        string GetUserNameWhithId(int id);
    }
}