using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTrackManager.Models
{
    public class Year : Screen
    {
        public Year(int year, bool isCurrent)
        {
            _number = year;
            _isCurrent = isCurrent;
        }
        public Year()
        {
        }

        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    IsCurrent = _number == DateTime.Now.Year;
                    NotifyOfPropertyChange(() => Number);
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
    }
}
