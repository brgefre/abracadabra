using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MtgOrganizer.Models;
using MtgOrganizer.DataAccess;

namespace MtgOrganizer.ViewModels
{
    public class MainViewModel : InputBoxOverlayViewModel
    {
        private XMLDAL _dal;

        #region Properties

        // Deck Lists
        public ObservableCollection<DeckList> DeckLists { get; private set; }
        public int DeckListIndex { get; set; } = 0;
        public DeckList SelectedDeckList { get; private set; }
        public Visibility DeckListDetailVisibility { get; private set; } = Visibility.Collapsed;
        
        // Decks
        public ObservableCollection<Deck> Decks { get { return SelectedDeckList.Decks; } }
        public int DeckIndex { get; set; } = 0;
        public Deck SelectedDeck { get; private set; }
        public Visibility DeckDetailVisibility { get; private set; } = Visibility.Collapsed;







        // Commands
        public ICommand CreateDeckListCommand { get; private set; }
        public ICommand UpdateDeckListCommand { get; private set; }
        public ICommand DeleteDeckListCommand { get; private set; }

        public ICommand CreateDeckCommand { get; private set; }
        public ICommand UpdateDeckCommand { get; private set; }
        public ICommand DeleteDeckCommand { get; private set; }




        #endregion






        #region Constructors

        public MainViewModel()
        {
            _dal = new XMLDAL(Properties.Settings.Default.DataDirectory);

            SelectedDeckList = new DeckList();
            SelectedDeck = new Deck();

            RefreshDeckLists();



            


            // Set commands
            CreateDeckListCommand = new RelayCommand<object>(new Action<object>(PromptToCreateDeckList));
            UpdateDeckListCommand = new RelayCommand<object>(new Action<object>(UpdateDeckList));
            DeleteDeckListCommand = new RelayCommand<object>(new Action<object>(DeleteDeckList));

            CreateDeckCommand = new RelayCommand<object>(new Action<object>(PromptToCreateDeck));
        }

        #endregion

        #region Methods: Deck Lists

        private void RefreshDeckLists()
        {
            DeckLists =_dal.GetDeckLists();
            OnPropertyChanged("DeckLists");
        }

        private void SortDeckLists()
        {
            string currentFilePath = SelectedDeckList.FilePath;
            List<DeckList> sorted = DeckLists.OrderBy(l => l.Name).ToList();

            DeckLists.Clear();
            DeckListIndex = 0;

            for (int i = 0; i < sorted.Count(); i++)
            {
                DeckLists.Add(sorted[i]);
                if (sorted[i].FilePath.Equals(currentFilePath)) DeckListIndex = i;
            }

            OnPropertyChanged("DeckLists");
            OnPropertyChanged("DeckListIndex");
        }

        public void SelectDeckList()
        {
            if (SelectedDeckList.HasUnsavedChanges || SelectedDeck.HasUnsavedChanges)
            {
                if (MessageBox.Show("You have unsaved changes that will be lost; proceed anyway?", 
                    "Warning! Unsaved Changes", MessageBoxButton.YesNo).Equals(MessageBoxResult.No))
                {
                    return;
                }
            }

            SelectedDeckList = DeckLists[DeckListIndex].Clone();
            DeckListDetailVisibility = Visibility.Visible;

            OnPropertyChanged("SelectedDeckList");
            OnPropertyChanged("DeckListDetailVisibility");
            OnPropertyChanged("Decks");
        }

        private void PromptToCreateDeckList(object parameter)
        {
            DisplayInputBox("Enter new deck list name", CreateDeckList);
        }

        private void CreateDeckList(string name)
        {
            DeckList newDeckList = new DeckList() { Name = name };
            _dal.CreateDeckList(newDeckList);
            RefreshDeckLists();
        }

        private void UpdateDeckList(object parameter)
        {
            SelectedDeckList.Update(_dal);
            DeckLists[DeckListIndex] = SelectedDeckList.Clone();
            SortDeckLists();
        }

        private void DeleteDeckList(object parameter)
        {
            if (MessageBox.Show("Are you sure you want to delete this deck list and all its decks?",
                "Delete?", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
            {
                // TODO



            }
        }

        #endregion

        #region Methods: Decks

        private void RefreshDecks()
        {



            OnPropertyChanged("Decks");
        }

        private void PromptToCreateDeck(object parameter)
        {
            DisplayInputBox("Enter new deck name", CreateDeck);
        }

        private void CreateDeck(string name)
        {
            Deck newDeck = new Deck() { DisplayName = name };
            _dal.CreateDeck(newDeck);
            RefreshDecks();
        }



        #endregion

    }
}