namespace cinefilo.Interfaces
{
    using System;

    public interface IEnterPasswordListener
    {
        void OnEnterPasswordClick(String password, bool? close);
    }
}
