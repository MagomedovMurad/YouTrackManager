using Caliburn.Micro;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YouTrackManager.Models;
using YouTrackManager.Utils;
using YouTrackSharp;
using YouTrackSharp.Projects;

namespace YouTrackManager.ViewModels
{
    public class TimeAccountingCalendarViewModel: Screen
    {
        public BindableCollection<string> DayNames { get; set; } = new BindableCollection<string>() { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"};
        public BindableCollection<Month> Months { get; set; } = new BindableCollection<Month>();
        public BindableCollection<Day> Days { get; set; } = new BindableCollection<Day>();

        private Year _year;
        public Year Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    NotifyOfPropertyChange(() => Year);
                }
            }
        }

        private Month _selectedMonth = new Month(DateTime.Now.Month);
        public Month SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    NotifyOfPropertyChange(() => SelectedMonth);
                    BuildCalendar(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));
                    UploadTimeTracking(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));
                }
            }
        }

        private Day _selectedDay;
        public Day SelectedDay
        {
            get
            {
                return _selectedDay;
            }
            set
            {
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    NotifyOfPropertyChange(() => SelectedDay);
                    NotifyOfPropertyChange(() => SelectedDayTime);
                }
            }
        }

        public string SelectedDayTime
        {
            get
            {
                return "Общее время: " + SelectedDay?.Time.Humanize(3).Replace(",","");
            }
        }

        public TimeAccountingCalendarViewModel()
        {
            Year = new Year(DateTime.Now.Year, true);
            Months.AddRange(GetMonths());
        }

        private List<Month> GetMonths()
        {
            var months = new List<Month>();
            var monthNames = new List<string> { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек" };

            int i = 1;
            foreach (var name in monthNames)
            {
                var month = new Month { Name = name };
                month.IsCurrent = DateTime.Now.Month == i;
                month.IsSelected = month.IsCurrent;
                month.MonthNumber = i;
                months.Add(month);
                i++;
            }
            return months;
        }

        public void BackYaerHandler()
        {
            Year.Number--;
            BuildCalendar(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));
            UploadTimeTracking(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));

        }

        public void ForwardYaerHandler()
        {
            Year.Number++;
            BuildCalendar(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));
            UploadTimeTracking(new DateTime(Year.Number, _selectedMonth.MonthNumber, 1));

        }

        private async Task UploadTimeTracking(DateTime dateTime)
        {
            DateTime dateWithOffset = GetDateWithOffset(dateTime);

            var connection = new UsernamePasswordConnection("http://ticket.infolan.org/", "m.magomedov", "5328469177531");
            var manager = new YoutrackManager(connection);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var allWorkItems = await manager.GetCustomWorkItems(dateWithOffset, dateWithOffset.AddDays(42)).ConfigureAwait(false);
            sw.Stop();
            var t = sw.Elapsed;
            foreach (var day in Days)
            {
                var dayWorkItems = allWorkItems.Where(x => x.Date.Date == day.Date);
                day.Time = SummWorkItemsTimes(dayWorkItems);

                var issues = dayWorkItems.GroupBy(x => x.Issue)
                                         .Select(x => new CustomIssue { Id = x.Key.Id, Summary = x.Key.Summary, Duration = SummWorkItemsTimes(x.ToList()) });

                day.Issues.AddRange(issues);
            }
        }

        public void IssueMouseLeftButtonDown(CustomIssue issue)
        {
            Process.Start(issue.Url);
        }

        private async Task BuildCalendar(DateTime dateTime)
        {
            var connection = new UsernamePasswordConnection("http://ticket.infolan.org/", "m.magomedov", "5328469177531");
            var manager = new YoutrackManager(connection);

            Days.Clear();
            DateTime date = GetDateWithOffset(dateTime);
            for (int i = 1; i <= 42; i++)
            {
                Day day = new Day { Date = date, DisplayName = date.DayOfWeek.ToString() };
                day.IsCurrent = date.Date == DateTime.Today;
                day.IsCurrentMonth = date.Month == SelectedMonth.MonthNumber;
                day.IsSelected = day.IsCurrent;//d.Date == DateTime.Today.AddDays(-1);
                Days.Add(day);
                date = date.AddDays(1);
            }
        }

        private int GetOffset(DayOfWeek dow)
        {
            return Convert.ToInt32(dow.ToString("D"));
        }

        private DateTime GetDateWithOffset(DateTime dateTime)
        {
            int offset = GetOffset(dateTime.DayOfWeek);

            if (offset != 0)
                return dateTime.AddDays(-offset + 1);
            else
                return dateTime.AddDays(-6);
        }

        private TimeSpan SummWorkItemsTimes(IEnumerable<CustomWorkItem> workItems)
        {
            TimeSpan duration = new TimeSpan();
            if (workItems != null)
                duration = new TimeSpan(workItems.Sum(r => r.Duration.Ticks));

            return duration;
        }
    }
}
