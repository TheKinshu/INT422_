using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {
            // If necessary, add constructor code here
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }
        public EmployeeBase EmployeeGetById(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null : Mapper.Map<Employee, EmployeeBase>(o);
        }

        public EmployeeBase EmployeeEdit(EmployeeEdit EEF)
        {
            var o = ds.Employees.Find(EEF.EmployeeId);

            if (o == null)
            {
                return null;

            }
            else
            {
                ds.Entry(o).CurrentValues.SetValues(EEF);
                ds.SaveChanges();
                return Mapper.Map<Employee, EmployeeBase>(o);
            }
        }

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks);
        }

        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            var Result = ds.Tracks;
            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(Result.Where(tn => tn.GenreId == 9).OrderBy(tn => tn.Name));

        }

        public IEnumerable<TrackBase> TrackGetAllDeepPurple()
        {
            var Result = ds.Tracks;

            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(Result.Where(cm => cm.Composer.Contains("Jon Lord")).OrderBy(cm => cm.TrackId));

        }

        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            var Result = ds.Tracks;

            return Mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(Result.OrderByDescending(mill => mill.Milliseconds).Take(100));

        }
    }
}