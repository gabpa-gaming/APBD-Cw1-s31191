namespace APBD_Cw1_s31191;

public class App(
    UserService userService,
    HardwareService hardwareService,
    LeaseService leaseService)
{
    public void Run()
    {
        InitializeRandomData();
        while (true)
        {
            ShowMenu();
            var choice = ReadInt("Wybierz opcję: ");

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    AddHardware();
                    break;
                case 3:
                    ShowAllHardware();
                    break;
                case 4:
                    ShowAvailableHardware();
                    break;
                case 5:
                    LeaseHardware();
                    break;
                case 6:
                    ReturnHardware();
                    break;
                case 7:
                    MarkHardwareUnavailable();
                    break;
                case 8:
                    ShowUserActiveLeases();
                    break;
                case 9:
                    ShowOverdueLeases();
                    break;
                case 10:
                    ShowSummaryReport();
                    break;
                case 0:
                    Console.WriteLine("Zamykanie aplikacji...");
                    return;
                default:
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("Wypożyczalnia sprzętu");
        Console.WriteLine("1. Dodaj nowego użytkownika");
        Console.WriteLine("2. Dodaj nowy sprzęt");
        Console.WriteLine("3. Pokaż cały sprzęt");
        Console.WriteLine("4. Pokaż sprzęt dostępny do wypożyczenia");
        Console.WriteLine("5. Wypożycz sprzęt");
        Console.WriteLine("6. Zwróć sprzęt");
        Console.WriteLine("7. Oznacz sprzęt jako niedostępny");
        Console.WriteLine("8. Pokaż aktywne wypożyczenia użytkownika");
        Console.WriteLine("9. Pokaż przeterminowane wypożyczenia");
        Console.WriteLine("10. Wygeneruj raport podsumowujący");
        Console.WriteLine("0. Wyjście");
        Console.WriteLine();
    }

    private void AddUser()
    {
        Console.WriteLine("Dodawanie użytkownika");
        Console.Write("Imię: ");
        var firstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Nazwisko: ");
        var lastName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Typ użytkownika:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Pracownik");
        var type = ReadInt("Wybierz typ: ");

        User user = type switch
        {
            1 => new Student(firstName, lastName),
            2 => new Employee(firstName, lastName),
            _ => throw new ArgumentException("Niepoprawny typ użytkownika.")
        };

        userService.AddUser(user);
        Console.WriteLine($"Dodano użytkownika: {user.FirstName} {user.LastName} (ID: {user.Id})");
    }

    private void AddHardware()
    {
        Console.WriteLine("Dodaj sprzęt");
        Console.Write("Nazwa: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Opis: ");
        var description = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Typ sprzętu:");
        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Projektor");
        Console.WriteLine("3. Kamera");
        var type = ReadInt("Wybierz typ: ");

        Console.WriteLine("Status:");
        Console.WriteLine("1. Dostępny");
        Console.WriteLine("2. Wypożyczony");
        Console.WriteLine("3. Niedostępny");
        var statusChoice = ReadInt("Wybierz status: ");

        var status = statusChoice switch
        {
            1 => AvailabilityStatus.Available,
            2 => AvailabilityStatus.Leased,
            3 => AvailabilityStatus.Unavailable,
            _ => throw new ArgumentException("Niepoprawny status.")
        };

        Hardware hardware = type switch
        {
            1 => CreateLaptop(name, description, status),
            2 => CreateProjector(name, description, status),
            3 => CreateCamera(name, description, status),
            _ => throw new ArgumentException("Niepoprawny typ sprzętu.")
        };

        hardwareService.AddHardware(hardware);
        Console.WriteLine($"Dodano sprzęt: {hardware.Name} (ID: {hardware.Id})");
    }

    private void ShowAllHardware()
    {
        Console.WriteLine("Cały sprzęt");
        PrintHardwareList(hardwareService.GetAllHardware());
    }

    private void ShowAvailableHardware()
    {
        Console.WriteLine("Sprzęt dostępny do wypożyczenia");
        var available = hardwareService.GetAllAvailableHardware();
        PrintHardwareList(available);
    }

    private void LeaseHardware()
    {
        Console.WriteLine("Wypożyczanie sprzętu");
        var userId = ReadInt("ID użytkownika: ");
        var hardwareId = ReadInt("ID sprzętu: ");
        var today = ReadDate("Data wypożyczenia (rrrr-mm-dd): ");
        var dueDate = ReadDate("Data zwrotu (rrrr-mm-dd): ");
        var baseFee = ReadDecimal("Opłata bazowa: ");
        var penaltyFee = ReadDecimal("Kara dzienna za opóźnienie: ");

        var result = leaseService.LeaseHardware(today, hardwareId, userId, dueDate, baseFee, penaltyFee);

        Console.WriteLine(result switch
        {
            LeaseResult.UserNotFound => "Nie znaleziono użytkownika.",
            LeaseResult.HardwareNotFound => "Nie znaleziono sprzętu.",
            LeaseResult.HardwareUnavailable => "Sprzęt jest niedostępny.",
            LeaseResult.UserLeaseLimitReached => "Użytkownik osiągnął limit aktywnych wypożyczeń.",
            LeaseResult.LeaseSuccess => "Wypożyczenie zakończone sukcesem.",
            _ => "Nieznany wynik."
        });
    }

    private void ReturnHardware()
    {
        Console.WriteLine("Zwrot sprzętu");
        var userId = ReadInt("ID użytkownika: ");
        var hardwareId = ReadInt("ID sprzętu: ");
        var today = ReadDate("Data zwrotu (rrrr-mm-dd): ");

        var (fee, result) = leaseService.ReturnLeasedHardware(today, hardwareId, userId);

        if (result == ReturnResult.ReturnSuccess)
        {
            Console.WriteLine($"Zwrot zakończony sukcesem. Naliczona opłata: {fee:0.00}");
        }
        else
        {
            Console.WriteLine("Nie znaleziono aktywnego wypożyczenia dla podanych danych.");
        }
    }

    private void MarkHardwareUnavailable()
    {
        Console.WriteLine("Oznaczanie sprzętu jako niedostępny");
        var id = ReadInt("ID sprzętu: ");
        var hardware = hardwareService.GetHardware(id);

        if (hardware is null)
        {
            Console.WriteLine("Nie znaleziono sprzętu.");
            return;
        }

        hardware.AvailabilityStatus = AvailabilityStatus.Unavailable;
        Console.WriteLine("Sprzęt oznaczono jako niedostępny.");
    }

    private void ShowUserActiveLeases()
    {
        Console.WriteLine("Aktywne wypożyczenia użytkownika");
        var userId = ReadInt("ID użytkownika: ");

        var leases =  leaseService.GetUserLeases(userId);
        
        PrintLeases(leases);
    }

    private void ShowOverdueLeases()
    {
        Console.WriteLine("Przeterminowane wypożyczenia");
        var today = DateTime.Today;
        var overdueLeases = leaseService.GetOverdueLeases(today);
        PrintLeases(overdueLeases);
    }

    private void PrintLeases(List<Lease> leases)
    {
        foreach (var lease in leases)
        {
            Console.WriteLine($"User Id: {lease.LesseeId}, Id Sprzetu: {lease.LeasedItemId}, Wypozyczono dnia: {lease.RentalDate}, Wypozyczono do: {lease.DueDate}");
        }
    }
    private void ShowSummaryReport()
    {
        var allHardware = hardwareService.GetAllHardware();
        var availableCount = allHardware.Count(h => h.AvailabilityStatus == AvailabilityStatus.Available);
        var leasedCount = allHardware.Count(h => h.AvailabilityStatus == AvailabilityStatus.Leased);
        var unavailableCount = allHardware.Count(h => h.AvailabilityStatus == AvailabilityStatus.Unavailable);

        Console.WriteLine("Podsumowanie");
        Console.WriteLine($"Łącznie sprzętów: {allHardware.Count}");
        Console.WriteLine($"Dostępne: {availableCount}");
        Console.WriteLine($"Wypożyczone: {leasedCount}");
        Console.WriteLine($"Niedostępne: {unavailableCount}");
    }

    private static void PrintHardwareList(List<Hardware> hardwareList)
    {
        if (hardwareList.Count == 0)
        {
            Console.WriteLine("Brak pozycji.");
            return;
        }

        foreach (var hardware in hardwareList)
        {
            Console.WriteLine($"ID: {hardware.Id} | {hardware.Name} | {hardware.Description} | Status: {hardware.AvailabilityStatus}");
        }
    }
    
    private static Laptop CreateLaptop(string name, string description, AvailabilityStatus status)
    {
        var processor = ReadString("Procesor: ");
        var screenSize = ReadDouble("Przekątna ekranu (cale): ");
        return new Laptop(name, description, status, processor, screenSize);
    }

    private static Projector CreateProjector(string name, string description, AvailabilityStatus status)
    {
        var aspectRatio = ReadDouble("Aspect ratio (jako ułamek dziesiętny):");
        var brightness = ReadInt("Jasność (lumeny): ");
        return new Projector(name, description, status, aspectRatio, brightness);
    }

    private static Camera CreateCamera(string name, string description, AvailabilityStatus status)
    {
        var lensKind = ReadString("Rodzaj obiektywu: ");
        var megapixels = ReadInt("Megapixele: ");
        return new Camera(name, description, status, lensKind, megapixels);
    }

    private static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (double.TryParse(input, out var value))
            {
                return value;
            }

            Console.WriteLine("Podaj poprawną liczbę.");
        }
    }
    
    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var value))
            {
                return value;
            }

            Console.WriteLine("Podaj poprawną liczbę.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (decimal.TryParse(input, out var value))
            {
                return value;
            }

            Console.WriteLine("Podaj poprawną wartość liczbową.");
        }
    }

    private static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (DateTime.TryParse(input, out var value))
            {
                return value;
            }

            Console.WriteLine("Podaj poprawną datę.");
        }
    }
    private void InitializeRandomData()
    {
        userService.AddUser(new Student("Jan", "Kowalski"));
        userService.AddUser(new Student("Ala", "Nowak"));
        userService.AddUser(new Employee("Piotr", "Wiśniewski"));
        userService.AddUser(new Employee("Anna", "Zielińska"));

        hardwareService.AddHardware(new Laptop("Laptop Dell", "Laptop biurowy", AvailabilityStatus.Available, "Intel i5", 15.6));
        hardwareService.AddHardware(new Laptop("Laptop HP", "Laptop gamingowy dysk 1000", AvailabilityStatus.Available, "AMD Ryzen 7", 14.0));
        hardwareService.AddHardware(new Projector("Projektor HP", "Projektor salowy", AvailabilityStatus.Available, 16.0, 3200));
        hardwareService.AddHardware(new Projector("Projektor Sony", "Projektor przenośny", AvailabilityStatus.Available, 16.0, 2500));
        hardwareService.AddHardware(new Camera("Kamera Logitech", "Kamera konferencyjna", AvailabilityStatus.Available, "Wide", 12.0));
        hardwareService.AddHardware(new Camera("Kamera Canon", "Kamera premium", AvailabilityStatus.Available, "Zoom", 24.0));

        leaseService.LeaseHardware(
            DateTime.Today.AddDays(-3),
            0,
            0,
            DateTime.Today.AddDays(2),
            100m,
            15m);

        leaseService.LeaseHardware(
            DateTime.Today.AddDays(-5),
            2,
            1,
            DateTime.Today.AddDays(-1),
            120m,
            20m);
    }
}
