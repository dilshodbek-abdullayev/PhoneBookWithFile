namespace PhoneBookWithFile.Services
{
    internal class UserInterfaceService
    {
        public void UserInterface()
        {
            FileService fileService = new FileService();
            try
            {
                bool isTrue = false;
                do
                {

                    Console.WriteLine("1 => Create Contact\n2 => Read Contact\n3 => Update Contact\n4 => Delete Contact");
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
                            Console.WriteLine("Please enter number between 1 and 4");
                            break;
                    }
                }
                while (IsTrue(isTrue));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
