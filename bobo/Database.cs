using System;
using System.Collections.Generic;
namespace bobo.Service{
    public static class DummyDatabase{
        public static List<Student>  Students = new List<Student>(){
            new Student{
                Given ="Mary",
                Surname ="RiMa",
                Id= Guid.NewGuid().ToString()
            },
            new Student{
                Given ="Qua",
                Surname ="Map",
                Id= Guid.NewGuid().ToString()
            }
        };

        public static List<UserCred> UserCreds = new List<UserCred>{
        };
    }
}