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
        public UsernamePasswordConnection _connection;

        public YoutrackManager(UsernamePasswordConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<CustomWorkItem>> GetCustomWorkItems(DateTime from, DateTime to)
        {

            var projectService = _connection.CreateProjectsService();
            var issueService = _connection.CreateIssuesService();
            var timeTrackingService = _connection.CreateTimeTrackingService();

            var projects = await projectService.GetAccessibleProjects();

            var tasks = projects.Select(p => GetCustomWorkItemsForProject(issueService, timeTrackingService, p, from, to)).ToArray();
            var customWorkItems = await Task.WhenAll(tasks).ConfigureAwait(false);

            return customWorkItems.SelectMany(x => x).ToList();
        }

        private async Task<List<CustomWorkItem>> GetCustomWorkItemsForProject(IssuesService issueService, TimeTrackingService timeTrackingService, Project project, DateTime from, DateTime to)
        {
            var issues = new List<Issue>();
            try
            {
                issues.AddRange(await issueService.GetIssuesInProject(project.ShortName, $"обновлена: {from.ToString("yyyy-MM")} .. {to.ToString("yyyy-MM")}", take: 5000).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

            var workItemsTasks = issues.Select(i => GetCustomWorkItemsForIssue(timeTrackingService, i.Id, from, to)).ToArray();
            var customItems = await Task.WhenAll(workItemsTasks).ConfigureAwait(false);
            return customItems.SelectMany(x => x).ToList();
        }

        private async Task<List<CustomWorkItem>> GetCustomWorkItemsForIssue(TimeTrackingService timeTrackingService, string issueId, DateTime from, DateTime to)
        {
            var result = new List<CustomWorkItem>();
            try
            {
                var workItems = await timeTrackingService.GetWorkItemsForIssue(issueId).ConfigureAwait(false);
                return workItems
                    .Where(wi => wi.Author.Login == "m.magomedov"
                                && wi.Date >= from
                                && wi.Date <= to)
                    .Select(wi => new CustomWorkItem()
                    {
                        IssueId = issueId,
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
