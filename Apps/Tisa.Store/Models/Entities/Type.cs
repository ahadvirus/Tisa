using System.Collections.Generic;
using Tisa.Store.Models.Contracts;

namespace Tisa.Store.Models.Entities
{
    public class Type : BaseViewModel
    {
        public Type()
        {
            _name = string.Empty;
            _title = string.Empty;
            _element = string.Empty;
            Attributes = new List<Attribute>();
        }

        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _element;

        public string Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
                OnPropertyChanged(nameof(Element));
            }
        }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
