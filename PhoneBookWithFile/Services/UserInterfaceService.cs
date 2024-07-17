namespace PhoneBookWithFile.Services
{
    internal class UserInterfaceService
    {
        private LoggingService log;
        private ExceptionLoggingService exceptionLog;
        public UserInterfaceService()
        {
            this.log = new LoggingService();
            this.exceptionLog = new ExceptionLoggingService();
        }
        public void UserInterface()
        {
            FileService fileService = new FileService();
            bool isTrue = false;
            do
            {
                try
                {
                    log.Log("Hello.Please select an internship");
                    log.Log("1 => Create Contact\n2 => Read Contact\n3 => Update Contact\n4 => Delete Contact");
                    log.Log("Your choise => ");
                    string userInput = Console.ReadLine();
                    int inputUser = Convert.ToInt32(userInput);
                    switch (inputUser)
                    {
                        case 1:
                            fileService.AddName();
                            break;
                        case 2:
                            fileService.ReadPhoneNumber();
                            break;
                        case 3:
                            fileService.UpdatePhoneNumber();
                            break;
                        case 4:
                            fileService.DeleteContact();
                            break;
                        default:
                            log.Log("Please enter number between 1 and 4");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    exceptionLog.Log(ex.Message);
                }
            }
            while (IsTrue(isTrue));
        }
        private bool IsTrue(bool isTrue)
        {
            Console.WriteLine("Do you want to continue? (yes / no)");
            string checkWhile = Console.ReadLine().ToLower();
            isTrue = checkWhile is "yes" or "y";
            return isTrue;
        }
    }
}
