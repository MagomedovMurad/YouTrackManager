﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTrackManager.Models;

namespace YouTrackManager.ViewModels
{
    public class ShellViewModel: Screen
    {
        private TimeAccountingCalendarViewModel _calendar;
        public TimeAccountingCalendarViewModel Calendar
        {
            get
            {
                return _calendar;
            }

            set
            {
                if (_calendar != value)
                {
                    _calendar = value;
                    NotifyOfPropertyChange(() => Calendar);
                }
            }
        }

        private LogonViewModel _logon;
        public LogonViewModel Logon
        {
            get
            {
                return _logon;
            }

            set
            {
                if (_logon != value)
                {
                    _logon = value;
                    NotifyOfPropertyChange(() => Logon);
                }
            }
        }

        public ShellViewModel()
        {
            Calendar = new TimeAccountingCalendarViewModel();
            Logon = new LogonViewModel();
        }
    }
}
