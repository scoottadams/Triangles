namespace CherwellTriangles.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public interface ISetViewModel
    {
        IEnumerable<SelectListItem> Letters { get; }

        IEnumerable<SelectListItem> Numbers { get; }

        void Initialise();
    }

    public class SetViewModel : ISetViewModel
    {
        public char Letter { get; set; }

        public IEnumerable<SelectListItem> Letters { get; private set; }

        public int Number { get; set; }

        public IEnumerable<SelectListItem> Numbers { get; private set; }

        public void Initialise()
        {
            var letters = new List<char>
                              {
                                  'A',
                                  'B',
                                  'C',
                                  'D',
                                  'E',
                                  'F'
                              };
            var numbers = new List<int>
                              {
                                  1,
                                  2,
                                  3,
                                  4,
                                  5,
                                  6,
                                  7,
                                  8,
                                  9,
                                  10,
                                  11,
                                  12
                              };

            this.Letters = letters.Select(
                n => new SelectListItem { Selected = false, Text = n.ToString(), Value = n.ToString() }).ToList();

            this.Numbers = numbers.Select(
                n => new SelectListItem { Selected = false, Text = n.ToString(), Value = n.ToString() }).ToList();
        }
    }
}