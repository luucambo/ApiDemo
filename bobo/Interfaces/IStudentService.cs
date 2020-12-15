using System.Collections.Generic;

namespace bobo.Interfaces{

    public interface IStudentService
    {
        ServiceResponse<List<Student>> GetStudents();

        ServiceResponse<Student> SaveStudent(Student student);

        ServiceResponse DeleteStudent(string id);
       
    }
}