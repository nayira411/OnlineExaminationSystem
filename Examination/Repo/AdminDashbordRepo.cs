using Examination.Models;

namespace Examination.Repo
{
    public class AdminDashbordRepo
    {
        ExamContext context = new ExamContext();
        public int getStdsCount()
        {
            var res = context.Students.Count();
            return res;
        }
        public int getInsCount()
        {
            var res = context.Instructors.Count();
            return res;
        }
        public int getCoursesCount()
        {
            var res = context.Courses.Count();
            return res;
        }
        public List<string> getTracks()
        {
            List<string> tNames= new List<string>();
            var res = context.Tracks.ToList(); 
            foreach ( var track in res)
            {
                tNames.Add(track.Tname);
            }
            return tNames;
        }
        public List<int> GetTracksCount()
        {
            var tracksCount = new List<int>();
            var tracks = context.Tracks.ToList();

            foreach (var track in tracks)
            {
                var count = track.Students.Count;
                tracksCount.Add(count);
            }

            return tracksCount;
        }

    }
}
