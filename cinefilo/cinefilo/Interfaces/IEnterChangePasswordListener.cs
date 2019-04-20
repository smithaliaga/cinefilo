namespace cinefilo.Interfaces
{
    public interface IEnterChangePasswordListener
    {
        void OnEnterChangePasswordClick(string currentPassword, string newPassword, bool close);
    }
}
