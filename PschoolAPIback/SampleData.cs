using PschoolAPIback.DbPschoolContext;

namespace PschoolAPIback
{

    public class SampleData
    {
        public static void Initialize(PschoolContext context)
        {
            if(context.Parents.Any())
            {
                return;
            }
            if(context.Students.Any())
            {
                return;
            }

            context.Parents.AddRange(
                new Models.Parent
                {
                    FirstName = "AHMED",
                    LastName = "SALIM",
                    Username = String.Empty,
                    Email = "AHMED.SALIM@gmail.com",
                    Address = String.Empty,
                    PhoneOne = String.Empty,
                    PhoneWork = String.Empty,
                    PhoneHome = String.Empty,
                    SiblingCount = 3
                },
                new Models.Parent
                {
                    FirstName = "MOHAMED",
                    LastName = "KHALID",
                    Username = String.Empty,
                    Email = "MOHAMED.KHALID@gmail.com",
                    Address = String.Empty,
                    PhoneOne = String.Empty,
                    PhoneWork = String.Empty,
                    PhoneHome = String.Empty,
                    SiblingCount = 1
                },
                new Models.Parent
                {
                    FirstName = "ALI",
                    LastName = "MOHAMED",
                    Username = String.Empty,
                    Email = "ALI.SALAH@gmail.com",
                    Address = String.Empty,
                    PhoneOne = String.Empty,
                    PhoneWork = String.Empty,
                    PhoneHome = String.Empty,
                    SiblingCount = 1
                },
                new Models.Parent
                {
                    FirstName = "FATIMA",
                    LastName = "SULTAN",
                    Username = String.Empty,
                    Email = "FATIMA.SULTAN@gmail.com",
                    Address = String.Empty,
                    PhoneOne = String.Empty,
                    PhoneWork = String.Empty,
                    PhoneHome = String.Empty,
                    SiblingCount = 1
                },
                new Models.Parent
                {
                    FirstName = "Mohamed",
                    LastName = "Omer",
                    Username = "Mohamed",
                    Email = "m.omer89@gmail.com",
                    Address = "Abu Dhabi",
                    PhoneOne = "0567584858",
                    PhoneWork = "0576809476",
                    PhoneHome = String.Empty,
                    SiblingCount = 3
                }
            );
            context.SaveChanges();

            context.Students.AddRange(
                new Models.Student
                {
                    ParentId = 1,
                    FirstName = "Jack",
                    LastName = "Sparrow",
                    Username = "jacky",
                    Email = "jack.spar@gmail.com",
                    PhoneOne = "0574569476",
                    SiblingCount = 2
                },
                new Models.Student
                {
                    ParentId = 1,
                    FirstName = "John",
                    LastName = "Whick",
                    Username = "john",
                    Email = "john.bud@gmail.com",
                    PhoneOne = "0345569476",
                    SiblingCount = 4
                },
                new Models.Student
                {
                    ParentId = 3,
                    FirstName = "Marry",
                    LastName = "Jane",
                    Username = "marryjane",
                    Email = "marry_j@gmail.com",
                    PhoneOne = "0574345123",
                    SiblingCount = 3
                },
                new Models.Student
                {
                    ParentId = 2,
                    FirstName = "Andrew",
                    LastName = "Watkins",
                    Username = "andrewat",
                    Email = "andr_wat@gmail.com",
                    PhoneOne = "0574569145",
                    SiblingCount = 1
                },
                new Models.Student
                {
                    ParentId = 5,
                    FirstName = "Jenifer",
                    LastName = "Olsen",
                    Username = "jenny",
                    Email = "jeny_ol@gmail.com",
                    PhoneOne = "0574556712",
                    SiblingCount = 2
                }
            );
            context.SaveChanges();

        }
    }
}