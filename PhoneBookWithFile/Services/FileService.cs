namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileService
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService loggingService;
        public FileService()
        {
            this.loggingService = new LoggingService();

            EnsureFileExists();
        }
        public void AddName()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter phone number");
            string phoneNumber = Console.ReadLine();
            int lastId = GetLastId(filePath);
            int newId = lastId + 1;
            File.AppendAllText(filePath, newId + "." + name + "/" + phoneNumber + Environment.NewLine);
        }
        public void ReadPhoneNumber()
        {
            string[] phoneNumers = File.ReadAllLines(filePath);
            foreach (string phoneNumber in phoneNumers)
            {
                string[] strings = phoneNumber.Split("/");
                Console.WriteLine($"Name : {strings[0]} Number : {strings[1]}");
            }
        }
        public void UpdatePhoneNumber()
        {
            ReadPhoneNumber();
            Console.WriteLine("Qaysi Id kontaktni o'zgartirmoqchisiz");
            string userInput = Console.ReadLine();
            int userID = int.Parse(userInput);

            if (CheckId(filePath, userID))
            {
                Console.WriteLine("Enter name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter phone number");
                string phoneNumber = Console.ReadLine();

                try
                {
                    bool updated = UpdateById(filePath, userID, name, phoneNumber);
                    if (updated)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Id not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private bool UpdateById(string filePath, int userID, string? name, string? phoneNumber)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool updated = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(".");
                    if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                    {
                        if (id == userID)
                        {
                            lines[i] = userID + "." + name + "/" + phoneNumber;
                            updated = true;
                            break;
                        }
                    }
                }
                if (updated)
                {
                    File.WriteAllLines(filePath, lines);
                }
                return updated;
            }
            return false;
        }

        public void DeleteContact()
        {
            ReadPhoneNumber();
            Console.WriteLine("Qaysi Id kontaktni o'chirmoqchisiz");
            string userInput = Console.ReadLine();
            int userID = int.Parse(userInput);

            if (CheckId(filePath, userID))
            {
                try
                {
                    bool deleted = DeleteById(filePath, userID);
                    if (deleted)
                    {
                        Console.WriteLine("Deleted");
                    }
                    else
                    {
                        Console.WriteLine("Not Deleted");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private bool DeleteById(string filePath, int userID)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool found = false;
                var updatedLines = new List<string>();
                foreach (string line in lines)
                {
                    string[] parts = line.Split(".");
                    if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                    {
                        if (id == userID)
                        {
                            found = true;
                            continue;
                        }
                    }
                    updatedLines.Add(line);
                }
                if (found)
                {
                    File.WriteAllLines(filePath, updatedLines);
                }
                return found;
            }
            return false;
        }

        static bool CheckId(string filePath, int inputId)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(".");
                    if (lines.Length > 0 && int.TryParse(parts[0], out int id))
                    {
                        if (id == inputId)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static int GetLastId(string filePath)
        {
            int lastID = 0;
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    // Oxirgi qatordagi ID ni olish
                    string lastLine = lines[lines.Length - 1];

                    // ID ni ajratib olish
                    string[] parts = lastLine.Split('.');
                    if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                    {
                        lastID = id;
                    }
                }
            }
            return lastID;
        }
        private static void EnsureFileExists()
        {
            bool isFilePresent = File.Exists(filePath);

            if (isFilePresent is false)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
