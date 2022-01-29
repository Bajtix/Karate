using BrusLib;

namespace Karate {
    public struct DisplaySubject {
        public string name { get; set; }
        public Grade[] grades { get; }
        public DisplaySubject(string name, Grade[] grades) {
            this.name = name;
            this.grades = grades;
        }
    }
}