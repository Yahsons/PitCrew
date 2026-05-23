using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PitCrew.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        protected bool LockClosure = false;


        public void UnlockWindow()
        {
            LockClosure = false;
        }

        public void OnClose(WindowClosingEventArgs e)
        {
            if (LockClosure)
                e.Cancel = true;
        }
    }
}
