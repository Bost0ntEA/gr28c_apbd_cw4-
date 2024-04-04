using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsNameOrLastNameEmpty(firstName, lastName) && !IsEmailValid(email) && !IsAgeValid(dateOfBirth))
            {
                return false;
            }


            /*
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);*/

            var client = GetClientById(clientId);
            var user = new User 
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            user.SetClientType(client);
            
            if (!user.IsUserHasCreditLimit())
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        Boolean IsNameOrLastNameEmpty(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            return true;
        }

        Boolean IsEmailValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
            return true;
        }

        Boolean IsAgeValid(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            return true;
        }

        Client GetClientById(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }
    }
}
