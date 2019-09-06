using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOrganizer.Models
{
    public class Card
    {
        #region Properties

        public string Name { get; set; } = "";
        public int Quantity { get; set; } = 1;
        public string Cost { get; set; } = "";
        public int CMC { get; set; } = 0;
        public string ColorIdentity { get; set; } = "";
        public string Type { get; set; } = "";
        public string Text { get; set; } = "";
        public string ImagePath { get; set; } = "";

        public List<string> Subtypes { get; set; }
        public List<string> CustomGroupings { get; set; }

        #endregion

        #region Constructors

        public Card()
        {
            Subtypes = new List<string>();
            CustomGroupings = new List<string>();
        }

        #endregion

        #region Methods



        #endregion
    }
}