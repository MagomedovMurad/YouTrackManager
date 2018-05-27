using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTrackManager.Models
{
    public class Day: Screen
    {
        public BindableCollection<CustomIssue> Issues { get; set; } = new BindableCollection<CustomIssue>();

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyOfPropertyChange(() => Date);
                }
            }
        }

        public int DayOfMonth => Date.Day;

        private double _time;
        public double Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    NotifyOfPropertyChange(() => Time);
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

        private bool _isCurrentMonth;
        public bool IsCurrentMonth
        {
            get
            {
                return _isCurrentMonth;
            }
            set
            {
                if (_isCurrentMonth != value)
                {
                    _isCurrentMonth = value;
                    NotifyOfPropertyChange(() => IsCurrentMonth);
                }
            }
        }

        private bool _isHoliday;
        public bool IsHoliday
        {
            get
            {
                return _isHoliday;
            }
            set
            {
                if (_isHoliday != value)
                {
                    _isHoliday = value;
                    NotifyOfPropertyChange(() => IsHoliday);
                }
            }
        }
    }
}
