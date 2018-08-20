using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YouTrackManager.Models;
using YouTrackSharp;
using YouTrackSharp.Issues;
using YouTrackSharp.Projects;
using YouTrackSharp.TimeTracking;

namespace YouTrackManager.Utils
{
    public class YoutrackManager
    {
        private const string _hostAddress = "http://ticket.infolan.org/";

        public UsernamePasswordConnection _connection;

        public YoutrackManager(UsernamePasswordConnection connection)
        {
            _connection = connection;
        }

        public YoutrackManager(string login, string password)
        {
            _connection = new UsernamePasswordConnection(_hostAddress, login, password);
        }

        public async Task<string> CheckConnection()
        {
            try
            {
                var userService = new YouTrackSharp.Users.UserService(_connection);
                var info = await userService.GetCurrentUserInfo().ConfigureAwait(false);
                return null;
            }
            catch (UnauthorizedConnectionException ex)
            {
                return "Неверный логин или пароль. Возможно недоступен сервер";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<CustomWorkItem>> GetAllWorkItems()
        {

            var projectService = _connection.CreateProjectsService();
            var issueService = _connection.CreateIssuesService();
            var timeTrackingService = _connection.CreateTimeTrackingService();

            //var projects = await projectService.GetAccessibleProjects();
            var projects = new[] { new Project { ShortName = "SEVSAOT" }, new Project { ShortName = "DAR_DEV" } };

            var tasks = projects.Select(p => GetCustomWorkItemsForProject(issueService, timeTrackingService, p)).ToArray();
            var customWorkItems = await Task.WhenAll(tasks).ConfigureAwait(false);

            return customWorkItems.SelectMany(x => x).ToList();
        }

        private async Task<List<CustomWorkItem>> GetCustomWorkItemsForProject(IssuesService issueService, TimeTrackingService timeTrackingService, Project project)
        {
            var issues = new List<Issue>();
            try
            {
                issues.AddRange(await issueService.GetIssuesInProject(project.ShortName, take: 5000).ConfigureAwait(false)); 
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

            var workItemsTasks = issues.Select(i => GetCustomWorkItemsForIssue(timeTrackingService, i)).ToArray();
            var customItems = await Task.WhenAll(workItemsTasks).ConfigureAwait(false);
            return customItems.SelectMany(x => x).ToList();
        }

        private async Task<List<CustomWorkItem>> GetCustomWorkItemsForIssue(TimeTrackingService timeTrackingService, Issue issue)
        {
            var result = new List<CustomWorkItem>();
            try
            {
                var workItems = await timeTrackingService.GetWorkItemsForIssue(issue.Id).ConfigureAwait(false);
                return workItems
                    .Where(wi => wi.Author.Login == "m.magomedov")
                    .Select(wi => new CustomWorkItem()
                    {
                        Issue = issue,
                        Date = wi.Date.Value,
                        Duration = wi.Duration
                    }).ToList();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                return new List<CustomWorkItem>();
            }
        }

    }
}
