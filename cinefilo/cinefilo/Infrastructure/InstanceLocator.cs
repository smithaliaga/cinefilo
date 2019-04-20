using System;
using System.Collections.Generic;
using System.Text;

namespace cinefilo.Infrastructure
{
    //UTILIZADO PARA ENCONTRAR LA MAINVIEWMODEL

    using ViewModels;

    class InstanceLocator
    {
        public MainViewModel Main
        {
            get;
            set;
        }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
