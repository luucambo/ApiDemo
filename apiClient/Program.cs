using System;

using System.Text;
using Newtonsoft.Json;

namespace apiClient
{
    class Program
    {
        static Client client = new Client();
        static void Main(string[] args)
        {
            // register user
            var registerResult = client.Post("Authentication/register", new UserCred
            {
                Username = "test",
                Password = "test"
            }).Result;
            Console.WriteLine(registerResult);

            // authenticate
            var authenticateResult = client.Post("Authentication/authenticate", new UserCred
            {
                Username = "test",
                Password = "test"
            }).Result;
            Console.WriteLine(authenticateResult);

            // set jwt
            var deserializeAuthenticationResult = JsonConvert.DeserializeObject<ServiceResponse<string>>(authenticateResult);
            if (deserializeAuthenticationResult.Success)
            {
                client.SetToken(deserializeAuthenticationResult.Data);
            }
            while (true)
            {
                var cmd = Console.ReadLine();
                var result = "";
                switch (cmd)
                {
                    case "get":
                        result = GetStudent();
                        break;
                    case "post":
                        {
                            var student = new Student
                            {
                                Given = "Bo",
                                Surname = Guid.NewGuid().ToString(),
                                Id = ""
                            };

                            result = CreateStudent(student);
                            break;
                        }
                    case "update":
                        {
                            Console.WriteLine("Please enter student name");
                            var newName = Console.ReadLine();

                            Console.WriteLine("Please enter student id");
                            var studentId = Console.ReadLine();

                            var student = new Student
                            {
                                Given = "Bo",
                                Surname = newName,
                                Id = studentId
                            };

                            result = UpdateStudent(student);
                            break;
                        }
                    case "delete":
                        {
                            Console.WriteLine("Please enter student id");
                            var studentId = Console.ReadLine();
                            result = DeleteStudent(studentId);
                            break;
                        }
                }
                Console.WriteLine("RESULT: " + result);
            }

        }

        private static string CreateStudent(Student student)
        {
            var createStudentResult = client.Post("Student", student).Result;
            return createStudentResult;
        }

        private static string DeleteStudent(string studentId)
        {
            var deleteStudentResult = client.Delete($"Student/{studentId}").Result;
            return deleteStudentResult;
        }

        private static string UpdateStudent(Student student)
        {
            var updateStudent = client.Put("Student", student).Result;
            return updateStudent;
        }

        private static string GetStudent()
        {

            var getStudentResult = client.Get("Student").Result;
            return getStudentResult;
        }
    }
}
