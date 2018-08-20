using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTrackManager.Models;
using YouTrackManager.Utils;

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

        private Screen _window;
        public Screen Window
        {

            get
            {
                return _window;
            }
            set
            {
                if (_window != value)
                {
                    _window = value;
                    NotifyOfPropertyChange(() => Window);
                }
            }
        }

        public ShellViewModel()
        {
            //Calendar = new TimeAccountingCalendarViewModel(null);
            //Window = Calendar;

            var logon = new LogonViewModel();
            logon.LogonSuccess += LogonSuccess;
            Window = logon;
        }

        private void LogonSuccess(object sender, LogonEventArgs e)
        {
            Window = new TimeAccountingCalendarViewModel(e.Connection);
        }
    }
}
