using System;
using System.Collections.Generic;
using bobo.Interfaces;
using System.Linq;
using bobo.Utilities;

namespace bobo.Service
{
    public class StudentService : IStudentService
    {
        public ServiceResponse DeleteStudent(string id)
        {
            var result = new ServiceResponse();
            var student = DummyDatabase.Students.FirstOrDefault(p => p.Id == id);
            if (student != null)
            {
                DummyDatabase.Students.Remove(student);
            }
            result.Success = true;
            return result;
        }

        public ServiceResponse<List<Student>> GetStudents()
        {
            var result = new ServiceResponse<List<Student>>();
            result.Data = DummyDatabase.Students;
            result.Success = true;
            return result;
        }
        public ServiceResponse<Student> SaveStudent(Student student)
        {
            var result = new ServiceResponse<Student>();

            if (student == null)
            {
                result.AddMessage("Student null request");
                result.Success = false;
                return result;
            }

            if (string.IsNullOrEmpty(student.Id))
            {
                var saveStudentRequest = CloneHelper.Clone<Student>(student);
                saveStudentRequest.Id = Guid.NewGuid().ToString();
                DummyDatabase.Students.Add(saveStudentRequest);
                result.Data = saveStudentRequest;
            }
            else
            {
                var updatedStudent = DummyDatabase.Students.FirstOrDefault(p => p.Id == student.Id);
                if (updatedStudent != null)
                {
                    if (!string.IsNullOrEmpty(student.Surname))
                        updatedStudent.Surname = student.Surname;
                    if (!string.IsNullOrEmpty(student.Given))
                        updatedStudent.Given = student.Given;
                }
                result.Data = updatedStudent;
            }

            result.Success = true;
            return result;
        }
    }
}