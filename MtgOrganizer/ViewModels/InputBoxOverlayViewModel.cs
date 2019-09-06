using System;
using System.Windows;
using System.Windows.Input;

namespace MtgOrganizer.ViewModels
{
    public class InputBoxOverlayViewModel : ViewModelBase
    {
        public string InputBoxPrompt { get; private set; } = "";
        public Visibility InputBoxVisibility { get; set; } = Visibility.Collapsed;
        public string InputBoxValue { get; set; } = "";
        public ICommand InputBoxYesCommand { get; protected set; }
        public ICommand InputBoxNoCommand { get; protected set; }
        protected delegate void InputBoxHandler(string input);
        protected InputBoxHandler _inputBoxHandler;

        public InputBoxOverlayViewModel() : base()
        {
            InputBoxYesCommand = new RelayCommand<object>(new Action<object>(InputBoxYesClicked));
            InputBoxNoCommand = new RelayCommand<object>(new Action<object>(InputBoxNoClicked));
        }

        protected void InputBoxYesClicked(object parameter)
        {
            _inputBoxHandler.Invoke(InputBoxValue);

            InputBoxVisibility = Visibility.Hidden;
            OnPropertyChanged("InputBoxVisibility");
        }

        protected void InputBoxNoClicked(object parameter)
        {
            InputBoxVisibility = Visibility.Collapsed;
            OnPropertyChanged("InputBoxVisibility");
        }

        protected void DisplayInputBox(string prompt, InputBoxHandler handler)
        {
            InputBoxPrompt = prompt;
            InputBoxValue = string.Empty;
            InputBoxVisibility = Visibility.Visible;

            _inputBoxHandler = handler;

            OnPropertyChanged("InputBoxPrompt");
            OnPropertyChanged("InputBoxValue");
            OnPropertyChanged("InputBoxVisibility");
        }
    }
}