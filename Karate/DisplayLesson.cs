using BrusLib;

namespace Karate {
    public class DisplayLesson {
        public int LessonNo { get; }
        public string LessonName { get; }
        public string StartsAt { get; }
        public string EndsAt { get; }
        
        public bool IsReplacement { get; }
        public bool IsCancelled { get; }
        
        public string Time { get; }
        public string Teacher { get; }
        
        public DisplayLesson(Lesson l) {
            LessonName = l.name;
            Teacher = l.teacher;
            LessonNo = l.lessonNum;
            StartsAt = l.from.ToString("HH:mm");
            EndsAt = l.to.ToString("HH:mm");
            IsReplacement = l.isReplacement;
            IsCancelled = l.isCancelled;
            Time = StartsAt + " - " + EndsAt;
        }
    }
}