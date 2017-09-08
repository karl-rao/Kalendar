using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.Data.Domain
{
    public class ProjectPack
    {
        public string Pagination { get; set; }

        public List<Project> Projects { get; set; }

        public ProjectPack()
        {
            Projects=new List<Project>();
        }

        public ProjectPack(string id, int pageSize, int pageNo,string controllerName,string actionName,int accountId=0)
        {
            var projects =
                Utility.DataCache.Project.CacheList()
                    .FindAll(o => o.Valid && (o.IsPublic || o.CreatorAccountId == accountId));
            switch ((id+"").ToUpper())
            {
                case "NEW":
                    projects = projects.OrderByDescending(o => o.CreateTime).ToList();
                    break;
                default:
                    projects = projects.OrderByDescending(o => o.SubscribedCount).ToList();
                    break;
            }

            var count = projects.Count;
            Projects = projects.Skip((pageNo - 1)*pageSize).Take(pageSize)
                .Select(o => new Project(o))
                .ToList();
            
            Pagination= HtmlHelper.RenderPagination(controllerName, actionName, count, pageSize, pageNo, "");
        }
    }
}
