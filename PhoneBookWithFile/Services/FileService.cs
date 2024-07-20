namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileService
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService log;
        public FileService()
        {
            this.log = new LoggingService();
            EnsureFileExists();
        }
        public void AddName()
        {
            log.LogInfo("Enter name");
            string name = Console.ReadLine();
            log.LogInfo("Enter phone number");
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
                log.LogInfo($"Name : {strings[0]} Number : {strings[1]}");
            }
        }
        public void UpdatePhoneNumber()
        {
            try
            {
                ReadPhoneNumber();
                log.LogInfo("Qaysi Id kontaktni o'zgartirmoqchisiz");
                string userInput = Console.ReadLine();
                int userID = int.Parse(userInput);

                if (CheckId(filePath, userID))
                {
                    log.LogInfo("Enter name");
                    string name = Console.ReadLine();
                    log.LogInfo("Enter phone number");
                    string phoneNumber = Console.ReadLine();

                    bool updated = UpdateById(filePath, userID, name, phoneNumber);
                    if (updated)
                    {
                        log.LogInfo("Updated");
                    }
                    else
                    {
                        log.LogInfo("Id not found");
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
        private bool UpdateById(string filePath, int userID, string? name, string? phoneNumber)
        {
            try
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
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            return false;
        }

        public void DeleteContact()
        {
            ReadPhoneNumber();
            log.LogInfo("Qaysi Id kontaktni o'chirmoqchisiz");
            string userInput = Console.ReadLine();
            int userID = int.Parse(userInput);

            if (CheckId(filePath, userID))
            {
                try
                {
                    bool deleted = DeleteById(filePath, userID);
                    if (deleted)
                    {
                        log.LogInfo("Deleted");
                    }
                    else
                    {
                        log.LogInfo("Not Deleted");
                    }
                }
                catch (Exception ex)
                {
                    log.LogError(ex.Message);
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

        public string AddContact(string name, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                log.LogError("Ism yoki telefon raqam xato kiritildi.To'g'ri qiymatlar kiriting");

                return "";
            }
            else
            {
                string formattedContact = $"{name},{phoneNumber}";
                File.AppendAllText(filePath, formattedContact);

                return formattedContact;
            }
        }

        public void ReadContact()
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] res = line.Split("/");
                if (res.Length > 0)
                {
                    log.LogInfo($"Name {res[0]} phone number {res[1]}");
                }
            }
        }
    }
}
