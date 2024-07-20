namespace PhoneBookWithFile.Services
{
    internal interface IFileService
    {
        void AddName();
        void ReadPhoneNumber();
        void UpdatePhoneNumber();
        void DeleteContact();
        string AddContact(string name, string phoneNumber);
        void ReadContact();
    }
}