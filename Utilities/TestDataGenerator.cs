using System;

namespace AutomationDemo.Utilities
{
    public static class TestDataGenerator
    {

        private static String firstName;
        private static String lastName;

        public static string GenerateName()
        {
            firstName = Faker.Name.First();
            lastName = Faker.Name.Last();
            return firstName + " " + lastName;
        }

        public static string GenerateAddress()
        {
            return Faker.Address.StreetAddress();
        }

        public static string GenerateEmail()
        {
            if (firstName == null || lastName == null)
            {
                GenerateName();
            }
            return firstName.ToLower() + "." + lastName.ToLower() + "@example.com";
        }

        public static string GeneratePhoneNumber()
        {
            return Faker.Phone.Number();
        }
    }
}
