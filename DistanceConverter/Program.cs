DistanceConverter();

// all the actions are being executed here
static void DistanceConverter()
{
    string defaultFilePath = (AppDomain.CurrentDomain.BaseDirectory).Replace("\\bin\\Debug\\net6.0", "") + "Data\\results.txt";
    var kmString = "";
    float kmFloat = 0;
    float m = 0;
    string result = "";
    float cont = -1;

    while (cont == -1)
    {
        Console.WriteLine("Enter distance in kilometers:");
        kmString = Console.ReadLine();
        if (kmString == "")
        {
            Console.WriteLine("The entered value was null!\n" +
                "Please, try again.");
        }
        else if (!float.TryParse(kmString, out kmFloat))
        {
            Console.WriteLine($"The entered value ({kmString}) couldn't be converted to float.\n" +
                "You must enter a valid number.\n" +
                "Please, try again.");
        }
        else if (kmString.Contains(","))
        {
            Console.WriteLine($"The entered value ({kmString}) couldn't be converted to float.\n" +
                "You must use a dot instead of a coma in order for the conversion to success.\n" +
                "Please, try again.");
        }
        else
        {
            m = kmFloat * 1000;
            result = $"{kmFloat} | {m}";
            Console.WriteLine($"The result - {kmFloat} km = {m} m\n");
            cont = 1;
        }

    }

    SaveResult(defaultFilePath, result);
}


// saves the result to the file (default file in the Data folder/file given by user)
// used in DistanceConverter
static void SaveResult(string defaultFilePath, string result)
{
    Console.WriteLine("Would you like the result to be written in:\n" +
        $"1) The default file (Location - {defaultFilePath}\n" +
        "2) Another file of your choice\n");
    var option = Console.ReadLine();

    while(option != "1" && option != "2")
    {
        Console.WriteLine($"Option {option} is not recognized.\n" +
            $"Please, try again!");
        option = Console.ReadLine();
    }

    if(option == "1")
    {
        WriteToFile(defaultFilePath, result);
    }
    if (option == "2")
    {
        Console.WriteLine("Please, enter the path for the file:");
        var filePath = Console.ReadLine();

        while (!File.Exists(filePath))
        {
            Console.WriteLine("File not found!\n" +
                "Please, try to enter the path again:");
            filePath = Console.ReadLine();
        }
        WriteToFile(filePath, result);
    }
}


// a separate method for writing to the file
// purpose - making SaveResult less cluttered
// used in SaveResult
static async void WriteToFile(string filePath, string result)
{
    if (new FileInfo(filePath).Length == 0)
    {
        await File.WriteAllTextAsync(filePath, result + "\n");
        Console.WriteLine("Result was written to the file successfully!\n");
        PerformAnother();
    }
    else
    {
        Console.WriteLine("It appears the file isn't empty!\n");
        Console.WriteLine(File.ReadAllText(filePath));
        Console.WriteLine("Would you like to erase this data from the file?\n" +
            "(Yes - enter [1] No - enter [2]):");
        var option2 = Console.ReadLine();
        if (option2 == "1")
        {
            await File.WriteAllTextAsync(filePath, result + "\n");
            PerformAnother();
        }
        else if (option2 == "2")
        {
            using StreamWriter file = new(filePath, append: true);
            await file.WriteLineAsync(result);
        }
        Console.WriteLine("Result was written to the file successfully!\n");
        PerformAnother();
    }
}


// performs the application again
// used in DistanceConverter
static void PerformAnother()
{
    Console.WriteLine("Would you like to perform another convertion? " +
        "(Yes - enter [1] No - enter anything else)\n");
    string option = Console.ReadLine();
    if (option == "1")
    {
        System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\DistanceConverter.exe");
        Environment.Exit(0);
    }
    else
    {
        Environment.Exit(0);
    }
}

//Console.ReadLine();