using Examination.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination.Repo
{
    public class TrackCourseRepo
    {
  
        public Dictionary<Track, List<Course>> GetTrackCoursesDictionary()
        {
            using (var context = new ExamContext()) 
            {
                var trackCourses = context.TrackCourses.Include(tc => tc.Cr).ToList();
                var trackCoursesDictionary = new Dictionary<Track, List<Course>>();

                foreach (var trackCourse in trackCourses)
                {
                    var track = context.Tracks.FirstOrDefault(t => t.TId == trackCourse.TId);
                    if (track != null)
                    {
                        if (!trackCoursesDictionary.ContainsKey(track))
                        {
                            trackCoursesDictionary[track] = new List<Course>();
                        }

                        trackCoursesDictionary[track].Add(trackCourse.Cr);
                    }
                }

                return trackCoursesDictionary;
            }
        }
        }
    }

