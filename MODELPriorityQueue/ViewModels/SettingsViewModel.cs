using MODELPriorityQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace MODELPriorityQueue.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private Manager newManager;
        public Manager NewManager
        {
            get { return newManager; }
            set { Set(() => NewManager, ref newManager, value); }
        }

        public SettingsViewModel()
        {
            NewManager = new Manager();
        }

        public async Task AddManager()
        {
            await NewManager.Add();
        }
    }
}
