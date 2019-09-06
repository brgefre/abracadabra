using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtgOrganizer.DataAccess;

namespace MtgOrganizer.Models
{
    #region Enums

    public enum Format
    {
        Standard,
        Commander,
        Oathbreaker
    }

    public enum GroupingType
    {
        CardType,
        Custom
    }

    #endregion

    public class Deck
    {
        #region Properties

        public bool HasUnsavedChanges { get; set; } = false;

        // General descriptive information
        public string FilePath { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public Format Format { get; set; } = Format.Standard;
        public string Description { get; set; } = "";

        public string ColorIdentity { get; set; } = "";

        public List<Card> Cards { get; set; }
        public int CardCount { get { return Cards.Count(); } }

        // Commander(s), oathbreaker(s), and/or signature spell(s)
        public Card PrimaryCard1 { get; set; }
        public Card PrimaryCard2 { get; set; }
        public Card SecondaryCard1 { get; set; }
        public Card SecondaryCard2 { get; set; }

        // Statistics are calculated then saved into properties
        public int MinimumCMC { get; private set; }
        public int MaximumCMC { get; private set; }
        public decimal MedianCMC { get; private set; }
        public decimal AverageCMC { get; private set; }
        public int CountOfArtifacts { get; private set; }
        public int CountOfCreatures { get; private set; }
        public int CountOfEnchantments { get; private set; }
        public int CountOfInstants { get; private set; }
        public int CountOfLands { get; private set; }
        public int CountOfPlaneswalkers { get; private set; }
        public int CountOfSorceries { get; private set; }

        // Cards within the deck are separated out into one or more groupings
        public GroupingType DefaultGroupingType { get; set; } = GroupingType.CardType;
        public GroupingType CurrentGroupingType { get; set; } = GroupingType.CardType;
        public List<Grouping> Groupings { get; set; }

        #endregion

        #region Constructors

        public Deck()
        {
            Cards = new List<Card>();
            Groupings = new List<Grouping>();
        }

        #endregion

        #region General Methods

        public Deck Clone()
        {
            Deck deck = new Deck();

            deck.FilePath = this.FilePath;
            deck.DisplayName = this.DisplayName;
            deck.Format = this.Format;
            deck.Description = this.Description;


            deck.ColorIdentity = this.ColorIdentity;



            return deck;
        }

        public void Load()
        {

        }

        public void Update(XMLDAL dal)
        {

        }

        #endregion

        #region Statistical Methods

        public void RefreshStatistics()
        {
            CalculateConvertedManaCostStatistics();
            CalculateCountsByType();
        }

        private void CalculateConvertedManaCostStatistics()
        {
            List<Card> sorted = Cards.OrderByDescending(c => c.CMC).ToList();

            MinimumCMC = sorted.Last().CMC;
            MaximumCMC = sorted.First().CMC;

            int totalCMC = 0;
            int minMedianIndex = (int)decimal.Floor(CardCount/2.0m);
            int maxMedianIndex = (int)decimal.Ceiling(CardCount / 2.0m);

            for (int i = 0; i < CardCount; i++)
            {
                if (i == minMedianIndex || i == maxMedianIndex) MedianCMC += sorted[i].CMC;
                totalCMC += sorted[i].CMC;
            }

            if (minMedianIndex != maxMedianIndex) MedianCMC /= 2.0m;
            AverageCMC = (decimal)totalCMC / CardCount;
        }

        private void CalculateCountsByType()
        {
            CountOfArtifacts = 0;
            CountOfCreatures = 0;
            CountOfEnchantments = 0;
            CountOfInstants = 0;
            CountOfLands = 0;
            CountOfPlaneswalkers = 0;
            CountOfSorceries = 0;

            foreach (Card card in Cards)
            {
                if (card.Type.Equals("Artifact")) CountOfArtifacts++;
                if (card.Type.Equals("Creature")) CountOfCreatures++;
                if (card.Type.Equals("Enchantment")) CountOfEnchantments++;
                if (card.Type.Equals("Instant")) CountOfInstants++;
                if (card.Type.Equals("Land")) CountOfLands++;
                if (card.Type.Equals("Planeswalker")) CountOfPlaneswalkers++;
                if (card.Type.Equals("Sorcery")) CountOfSorceries++;
            }
        }

        #endregion
    }
}