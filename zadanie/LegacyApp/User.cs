using System;

namespace LegacyApp
{
    public class User
    {
        public object Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public bool IsUserHasCreditLimit()
        {
            if (HasCreditLimit && CreditLimit < 500)
            {
                return false;
            }
            return true;
        }
        public void SetClientType(Client client)
        {
            if ( client.Type == "VeryImportantClient")
            {
                HasCreditLimit = false;
            }
            else if ( client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(LastName, DateOfBirth);
                    creditLimit = creditLimit * 2;
                    CreditLimit = creditLimit;
                }
            }
            else
            {
                HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(LastName, DateOfBirth);
                    CreditLimit = creditLimit;
                }
            }
        }
        
    }
}