using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTrackManager.Models
{
    public class Month: Screen
    {
        public Month()
        {
        }

        public Month(int month)
        {
            _month = month;
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange(() => Name);
                }
            }
        }

        private bool _isCurrent;
        public bool IsCurrent
        {
            get
            {
                return _isCurrent;
            }
            set
            {
                if (_isCurrent != value)
                {
                    _isCurrent = value;
                    NotifyOfPropertyChange(() => IsCurrent);
                }
            }
        }

        private int _month;
        public int MonthNumber
        {
            get
            {
                return _month;
            }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    NotifyOfPropertyChange(() => MonthNumber);
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyOfPropertyChange(() => IsSelected);
                }
            }
        }
    }
}
