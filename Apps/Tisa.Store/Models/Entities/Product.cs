using System.Collections.Generic;
using System.Linq;
using Tisa.Store.Models.Contracts;

namespace Tisa.Store.Models.Entities
{
    public class Product : BaseViewModel
    {
        public Product()
        {
            _store = 0;

            _title = string.Empty;

            _count = string.Empty;

            _power = string.Empty;

            _unit = string.Empty;

            _description = string.Empty;
        }

        public Product(int store, List<KeyValuePair<int, string>> values)
        {
            _store = store;

            _title = values
                .Where(pair => pair.Key == 1)
                .Select(pair => pair.Value)
                .First();

            _count = values
                .Where(pair => pair.Key == 2)
                .Select(pair => pair.Value)
                .First();

            _power = values
                .Where(pair => pair.Key == 3)
                .Select(pair => pair.Value)
                .First();

            _unit = values
                .Where(pair => pair.Key == 4)
                .Select(pair => pair.Value)
                .First();

            _description = values
                .Where(pair => pair.Key == 5)
                .Select(pair => pair.Value)
                .First();
        }
        public int Id { get; set; }

        private int _store;

        public int Store
        {
            get
            {
                return _store;
            }
            set
            {
                _store = value;
                OnPropertyChanged(nameof(Store));
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

        private string _count;

        public string Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        private string _power;

        public string Power
        {
            get
            {
                return _power;
            }
            set
            {
                _power = value;
                OnPropertyChanged(nameof(Power));
            }
        }

        private string _unit;

        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }


        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool Valid
        {
            get
            {
                return !string.IsNullOrEmpty(Title) ||
                    !string.IsNullOrEmpty(Count) ||
                    !string.IsNullOrEmpty(Power) ||
                    !string.IsNullOrEmpty(Unit) ||
                    !string.IsNullOrEmpty(Description);
            }
        }
    }
}
