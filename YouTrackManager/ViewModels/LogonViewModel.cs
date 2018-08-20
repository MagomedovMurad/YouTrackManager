using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTrackManager.Utils;
using YouTrackManager.Views;
using YouTrackSharp;

namespace YouTrackManager.ViewModels
{
    public class LogonViewModel : Screen
    {
        public event EventHandler<LogonEventArgs> LogonSuccess;

        private const string _hostAddress = "http://ticket.infolan.org/";

        private LogonView _view;

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyOfPropertyChange(() => UserName);
                }
            }
        }

        public async Task LoginCommand()
        {
            var connection = new UsernamePasswordConnection(_hostAddress, _userName, _view.LoginPasswordBox.Password);

            var manager = new YoutrackManager(connection);
            var error = await manager.CheckConnection().ConfigureAwait(false);

            if (error == null)
                LogonSuccess?.Invoke(null, new LogonEventArgs(connection));
        }

        protected override void OnViewLoaded(object view)
        {
            _view = view as LogonView;
            //_view.LoginPasswordBox.Password;
           // base.OnViewLoaded(view);
        }

    }
}
