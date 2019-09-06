using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOrganizer.Models
{
    public class Grouping
    {
        #region Properties

        public string Name { get; set; }
        public List<Card> Cards { get; set; }

        #endregion

        #region Constructors

        public Grouping()
        {
            Cards = new List<Card>();
        }

        #endregion

        #region Methods



        #endregion
    }
}