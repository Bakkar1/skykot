using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KotClassLibrary.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Loading
        public bool isLoadingIconActive { get; set; } = true;
        public bool isContentActive { get; set; } = false;
        public bool IsLoadingIconActive
        {
            get { return isLoadingIconActive; }
            set
            {
                isLoadingIconActive = value;
                OnPropertyChanged(nameof(IsLoadingIconActive));
            }
        }
        public bool IsContentActive
        {
            get { return isContentActive; }
            set
            {
                isContentActive = value;
                OnPropertyChanged(nameof(IsContentActive));
            }
        }
        public void EnableLoding()
        {
            IsLoadingIconActive = true;
            IsContentActive = false;
        }
        public void DisableLoding()
        {
            IsLoadingIconActive = false;
            IsContentActive = true;
        }
        #endregion
    }
}
