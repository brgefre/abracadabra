using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MtgOrganizer.Models;

namespace MtgOrganizer.DataAccess
{
    public class XMLDAL
    {
        private string _dataDirectory;

        public XMLDAL(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        #region Methods: Create

        public void CreateDeckList(DeckList list)
        {
            string fileName = GetNextFilePath("decklist-");
            list.Serialize().Save(fileName);
        }

        public void CreateDeck(Deck deck)
        {
            string fileName = GetNextFilePath("deck-");
            deck.Serialize().Save(fileName);
        }

        private string GetNextFilePath(string fileNamePrefix)
        {
            string[] files = Directory.GetFiles(_dataDirectory, fileNamePrefix + "*.xml");
            string nextFilePath = _dataDirectory + @"\" + fileNamePrefix;
            int nextFileNumber = 1;

            if (files.Count() > 0)
            {
                for (int i = 1; true; i++)
                {
                    if (!files.Contains(nextFilePath + i.ToString() + ".xml"))
                    {
                        nextFileNumber = i;
                        break;
                    }
                }
            }

            return nextFilePath + nextFileNumber.ToString() + ".xml";
        }

        #endregion

        #region Methods: Read

        public ObservableCollection<DeckList> GetDeckLists()
        {
            List<DeckList> lists = new List<DeckList>();

            string[] files = Directory.GetFiles(_dataDirectory, "decklist-*.xml");

            foreach (string filePath in files)
            {
                DeckList deckList = new DeckList();    
                XDocument doc = XDocument.Load(filePath);
                XElement root = doc.Root;

                if (root.Name.ToString().Equals("decklist"))
                {
                    deckList.Name = (string)doc.Root.Element("name");
                    deckList.FilePath = filePath;

                    IEnumerable<XElement> decks = from el in root.Descendants("deck") select el;

                    foreach (XElement element in decks)
                    {
                        Deck deck = new Deck();

                        deck.FilePath = (string)element.Element("filepath");
                        deck.DisplayName = (string)element.Element("filepath");

                        deckList.Decks.Add(deck);
                    }

                    lists.Add(deckList);
                }
            }

            // Sort the collection by display name
            lists = lists.OrderBy(l => l.Name).ToList();

            ObservableCollection<DeckList> sorted = new ObservableCollection<DeckList>();

            foreach (DeckList d in lists)
            {
                sorted.Add(d);
            }

            // Return it
            return sorted;
        }







        #endregion

        #region Methods: Update

        public void UpdateDeckList(DeckList list)
        {
            list.Serialize().Save(list.FilePath);
        }

        public void UpdateDeck(Deck deck)
        {
            deck.Serialize().Save(deck.FilePath);
        }

        #endregion

        #region Methods: Delete



        #endregion
    }
}
