using PhoneBookWithFile.Services;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterfaceService userInterfaceService = new UserInterfaceService();
            userInterfaceService.UserInterface();

            /*  IFileService fileService = new FileService();
              fileService.AddContact("", "+998976543210");
              fileService.ReadContact();*/

        }
    }
}
