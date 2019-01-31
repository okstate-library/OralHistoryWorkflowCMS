using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace WpfApp.Domain
{
    public class DemoItem : INotifyPropertyChanged
    {
        private string _name;
        private ICommand _commond;

        public DemoItem(string name, ICommand iCommand )
        {
            _name = name;
        }
        
        public ICommand Commond
        {
            get { return _commond; }
            set { this.MutateVerbose(ref _commond, value, RaisePropertyChanged()); }
        }

        public string Name
        {
            get { return _name; }
            set { this.MutateVerbose(ref _name, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
