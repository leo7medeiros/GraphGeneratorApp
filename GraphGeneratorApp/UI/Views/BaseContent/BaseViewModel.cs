using Mopups.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GraphGeneratorApp.UI.Views.BaseContent
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {

        }

        private Page _currentPage = null;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public static void FecharPopup(bool allAsync)
        {
            if (MopupService.Instance.PopupStack.Count > 0)
            {
                if (allAsync)
                {
                    MopupService.Instance.PopAllAsync(false);
                }
                else
                {
                    MopupService.Instance.PopAsync(false);
                }
            }
        }

        #region INOTIFYPROPERTYCHANGED

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}