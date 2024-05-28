using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumTest.SeleniumTest
{
    public static class Program
    {
        private const string Header = "Iteration;Time;Memory;ProcessorTime;";
        private static readonly string BaseDirectory = Directory.GetCurrentDirectory();
        private static readonly string AngularResults = Path.Combine(BaseDirectory, "AngularTestResults");
        private static readonly string BlazorResults = Path.Combine(BaseDirectory, "BlazorTestResults");

        public static void Main()
        {
            Console.WriteLine("=/=/=/=/=/=/=/=START/=/=/=/=/=/=/=/=/");

            var testCases = new List<TestCase>
            {
                new() { Name = "Angular", Url = "http://localhost:44432", ResultsDirectory = AngularResults },
                new() { Name = "Blazor", Url = "http://localhost:5046", ResultsDirectory = BlazorResults }
            };

            foreach (var testCase in testCases)
            {
                Console.WriteLine($"=/=/=/=/=/Starting Test for {testCase.Name}/=/=/=/=/=/=/=/=/");
                InitializeFiles(testCase.ResultsDirectory);

                for (var i = 1; i < 101; i++)
                {
                    ExecuteTests(i, testCase);
                }
            }

            Console.WriteLine("=/=/=/=/=/=/=/=/=END=/=/=/=/=/=/=/=/=");
        }

        private static void InitializeFiles(string resultsDirectory)
        {
            if (!Directory.Exists(resultsDirectory))
            {
                Directory.CreateDirectory(resultsDirectory);
            }

            InitializeFile("primeFile.csv", Header, resultsDirectory);
            InitializeFile("warmStartListPageFile.csv", Header, resultsDirectory);
            InitializeFile("setColorListPageFile.csv", Header, resultsDirectory);
            InitializeFile("filterListPageFile.csv", Header, resultsDirectory);
            InitializeFile("InitialLoadTimesFile.csv", "Iteration;primePage;ListPage;", resultsDirectory);
        }

        private static void InitializeFile(string fileName, string header, string resultsDirectory)
        {
            var filePath = Path.Combine(resultsDirectory, fileName);
            using var writer = new StreamWriter(filePath);
            writer.WriteLine(header);
        }

        private static void ExecuteTests(int iteration, TestCase testCase)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"Iteration {iteration}");
            Console.WriteLine("=====================================");


            var primeFilePath = Path.Combine(testCase.ResultsDirectory, "primeFile.csv");
            var setColorListPageFilePath = Path.Combine(testCase.ResultsDirectory, "setColorListPageFile.csv");
            var filterListPageFilePath = Path.Combine(testCase.ResultsDirectory, "filterListPageFile.csv");
            var warmStartListPageFilePath = Path.Combine(testCase.ResultsDirectory, "warmStartListPageFile.csv");
            var initialLoadTimesFilePath = Path.Combine(testCase.ResultsDirectory, "InitialLoadTimesFile.csv");



            long? initialLoadTimeprimePage = null; 
            
            long? initialLoadTimeListPage = null; 
            
             initialLoadTimeListPage =
                 ExecuteListPageModificationTest(iteration, setColorListPageFilePath, filterListPageFilePath,
                     testCase.Url);
             initialLoadTimeprimePage = ExecutePrimeTest(iteration, primeFilePath, testCase.Url);

            ExecuteListPageWarmStartTest(iteration, warmStartListPageFilePath, testCase.Url);

            var dataLine = $"{iteration};{initialLoadTimeprimePage}ms;{initialLoadTimeListPage}ms;";
            AppendDataToFile(initialLoadTimesFilePath, dataLine);
        }

        private static long ExecutePrimeTest(int i, string filePath, string url)
        {
            using IWebDriver driver = new ChromeDriver();
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Prime Page");

            var initialLoadTime = NavigateToUrl(driver, url + "/primePage", "primebtn");

            var calculateButton = driver.FindElement(By.Id("primebtn"));

            var calcTracker = new Tracker();

            calcTracker.SetupAndStartMonitoring();

            calcTracker.StartTime();
            calculateButton.Click();
            calcTracker.StopTime();

            var time = $"{calcTracker.GetTime()}ms";
            var memory = $"{calcTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{calcTracker.GetCpuUsage()}ms";

            string dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(filePath, dataLine);

            Console.WriteLine($"  Calculation time taken    : {calcTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {calcTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {calcTracker.GetCpuUsage()} ms");

            return initialLoadTime;
        }
        

        private static long ExecuteListPageModificationTest(int i, string setColorListPageFilePath,
            string filterListPageFilePath, string url)
        {
            using IWebDriver driver = new ChromeDriver();
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for List Page");

            var initialLoadTime = NavigateToUrl(driver, url + "/listPage" , "togglebrandbtn");

            ExecuteToggle(driver, i, filterListPageFilePath);
            ExecuteColorSet(driver, i, setColorListPageFilePath);

            return initialLoadTime;
        }

        private static void ExecuteColorSet(IWebDriver driver, int i, string setColorListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for ColorSet");

            var setColorButton = driver.FindElement(By.Id("setcolorchromabtn"));

            var colorTestTracker = new Tracker();
            colorTestTracker.SetupAndStartMonitoring();

            colorTestTracker.StartTime();
            setColorButton.Click();
            colorTestTracker.StopTime();

            var time = $"{colorTestTracker.GetTime()}ms";
            var memory = $"{colorTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{colorTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(setColorListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {colorTestTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {colorTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {colorTestTracker.GetCpuUsage()} ms");
        }

        private static void ExecuteToggle(IWebDriver driver, int i, string filterListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Toggle");

            var toggleButton = driver.FindElement(By.Id("togglebrandbtn"));

            var toggleTestTracker = new Tracker();
            toggleTestTracker.SetupAndStartMonitoring();

            toggleTestTracker.StartTime();
            toggleButton.Click();
            toggleTestTracker.StopTime();

            var time = $"{toggleTestTracker.GetTime()}ms";
            var memory = $"{toggleTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{toggleTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(filterListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {toggleTestTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {toggleTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {toggleTestTracker.GetCpuUsage()} ms");
            Console.WriteLine("-------------------------------------");
        }

        private static void ExecuteListPageWarmStartTest(int i, string warmStartListPageFilePath, string url)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Warm start LoadTime");
            using IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url + "/listPage");
            driver.Navigate().GoToUrl(url);

            var warmStartTestTracker = new Tracker();
            warmStartTestTracker.SetupAndStartMonitoring();

            warmStartTestTracker.StartTime();
            driver.Navigate().GoToUrl(url + "/listPage");
            warmStartTestTracker.StopTime();

            var time = $"{warmStartTestTracker.GetTime()}ms";
            var memory = $"{warmStartTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{warmStartTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(warmStartListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {warmStartTestTracker.GetTime()} ms");
            Console.WriteLine(
                $"  Physical memory usage     : {warmStartTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {warmStartTestTracker.GetCpuUsage()} ms");
            Console.WriteLine("-------------------------------------");
        }

        private static long NavigateToUrl(IWebDriver driver, string url, string? load = null)
        {
            var initTracker = new Tracker();
            initTracker.SetupAndStartMonitoring();

            initTracker.StartTime();
            driver.Navigate().GoToUrl(url);

            if (load is not null)
            {
                try
                {
                    new WebDriverWait(driver, TimeSpan.FromSeconds(200)).Until(
                        ExpectedConditions.ElementToBeClickable(By.Id(load)));
                }
                catch (NoSuchElementException e)
                {
                }
            }

            initTracker.StopTime();

            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for LoadTime");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  LoadTime for              : {url}");
            Console.WriteLine($"  LoadTime time taken       : {initTracker.GetTime()} ms");
            Console.WriteLine("-------------------------------------");

            return initTracker.GetTime();
        }

        private static void AppendDataToFile(string filePath, string data)
        {
            using var writer = new StreamWriter(filePath, true);
            writer.WriteLine(data);
        }
    }
}