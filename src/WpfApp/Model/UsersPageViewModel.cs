using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp
{
    public class UsersPageViewModel : ViewModelBase
    {
        public UsersPageViewModel(UsersPage model)
        {
            this.Model = model;
        }

        public UsersPage Model { get; private set; }

    }
}
