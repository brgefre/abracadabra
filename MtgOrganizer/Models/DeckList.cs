using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtgOrganizer.DataAccess;

namespace MtgOrganizer.Models
{
    public class DeckList
    {
        #region Properties

        public bool HasUnsavedChanges { get; set; } = false;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    HasUnsavedChanges = true;
                }
            }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    HasUnsavedChanges = true;
                }
            }
        }

        public ObservableCollection<Deck> Decks { get; set; }

        public int DeckCount { get { return Decks.Count(); } }

        #endregion

        #region Constructors

        public DeckList()
        {
            Decks = new ObservableCollection<Deck>();
        }

        #endregion

        #region Methods

        public DeckList Clone()
        {
            DeckList list = new DeckList();

            list.Name = this.Name;
            list.FilePath = this.FilePath;
            list.HasUnsavedChanges = false;

            foreach (Deck deck in this.Decks)
            {
                list.Decks.Add(deck.Clone());
            }

            return list;
        }

        public void Update(XMLDAL dal)
        {
            dal.UpdateDeckList(this);
            HasUnsavedChanges = false;
        }

        #endregion
    }
}