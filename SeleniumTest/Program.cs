using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumTest.SeleniumTest
{
    public static class Program
    {
        private const string Header = "Iteration;Time;Memory;ProcessorTime;";
        private static readonly string BaseDirectory = Directory.GetCurrentDirectory();
        private static readonly string ResultsDirectory = Path.Combine(BaseDirectory, "TestResults");

        public static void Main()
        {
            Console.WriteLine("=/=/=/=/=/=/=/=START/=/=/=/=/=/=/=/=/");

            InitializeFiles();

            for (var i = 1; i < 11; i++)
            {
                ExecuteTests(i);
            }

            Console.WriteLine("=/=/=/=/=/=/=/=/=END=/=/=/=/=/=/=/=/=");
        }

        private static void InitializeFiles()
        {
            if (!Directory.Exists(ResultsDirectory))
            {
                Directory.CreateDirectory(ResultsDirectory);
            }

            InitializeFile("fibonacciFile.csv", Header);
            InitializeFile("warmStartListPageFile.csv", Header);
            InitializeFile("setColorListPageFile.csv", Header);
            InitializeFile("filterListPageFile.csv", Header);
            InitializeFile("InitialLoadTimesFile.csv", "Iteration;FibonacciPage;ListPage;");
        }

        private static void InitializeFile(string fileName, string header)
        {
            var filePath = Path.Combine(ResultsDirectory, fileName);
            using var writer = new StreamWriter(filePath);
            writer.WriteLine(header);
        }

        private static void ExecuteTests(int iteration)
        {
            using IWebDriver driver = new ChromeDriver();
            Console.WriteLine("=====================================");
            Console.WriteLine($"Iteration {iteration}");
            Console.WriteLine("=====================================");

           
            var fibonacciFilePath = Path.Combine(ResultsDirectory, "fibonacciFile.csv");
            var setColorListPageFilePath = Path.Combine(ResultsDirectory, "setColorListPageFile.csv");
            var filterListPageFilePath = Path.Combine(ResultsDirectory, "filterListPageFile.csv");
            var warmStartListPageFilePath = Path.Combine(ResultsDirectory, "warmStartListPageFile.csv");
            var initialLoadTimesFilePath = Path.Combine(ResultsDirectory, "InitialLoadTimesFile.csv");

            var initialLoadTimeFibonacciPage = ExecuteFibonacciTest(driver, iteration, fibonacciFilePath);
            var initialLoadTimeListPage =
                ExecuteListPageModificationTest(driver, iteration, setColorListPageFilePath, filterListPageFilePath);
            
            ExecuteListPageWarmStartTest(iteration, warmStartListPageFilePath);
            
            var dataLine = $"{iteration};{initialLoadTimeFibonacciPage}ms;{initialLoadTimeListPage}ms;";
            AppendDataToFile(initialLoadTimesFilePath, dataLine);
        }

        private static long ExecuteFibonacciTest(IWebDriver driver, int i, string filePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Fibonacci Page");

            var initialLoadTime = NavigateToUrl(driver, "http://localhost:5046/fibonacciPage");

            WaitForPageLoad(4000);

            var calculateButton = driver.FindElement(By.Id("fibonaccibtn"));

            var calcTracker = new Tracker();

            calcTracker.SetupAndStartMonitoring();

            calcTracker.StartTime();
            calculateButton.Click();
            calcTracker.StopTime();

            var time = $"{calcTracker.GetTime()}ms";
            var memory = $"{calcTracker.GetMemoryUsage()}mb";
            var processorTime = $"{calcTracker.GetCpuUsage()}ms";

            string dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(filePath, dataLine);

            Console.WriteLine($"  Calculation time taken    : {calcTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {calcTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {calcTracker.GetCpuUsage()} ms");

            return initialLoadTime;
        }

        private static void WaitForPageLoad(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        private static long ExecuteListPageModificationTest(IWebDriver driver, int i, string setColorListPageFilePath,
            string filterListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for List Page");

            var initialLoadTime = NavigateToUrl(driver, "http://localhost:5046/listPage");
            WaitForPageLoad(4000);

            ExecuteToggle(driver, i, filterListPageFilePath);
            ExecuteColorSet(driver, i, setColorListPageFilePath);

            return initialLoadTime;
        }

        private static void ExecuteColorSet(IWebDriver driver, int i, string setColorListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for ColorSet");

            var setColorButton = driver.FindElement(By.Id("setcolorredbtn"));

            var colorTestTracker = new Tracker();
            colorTestTracker.SetupAndStartMonitoring();

            colorTestTracker.StartTime();
            setColorButton.Click();
            colorTestTracker.StopTime();

            var time = $"{colorTestTracker.GetTime()}ms";
            var memory = $"{colorTestTracker.GetMemoryUsage()}mb";
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
            var memory = $"{toggleTestTracker.GetMemoryUsage()}mb";
            var processorTime = $"{toggleTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(filterListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {toggleTestTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {toggleTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {toggleTestTracker.GetCpuUsage()} ms");
            Console.WriteLine("-------------------------------------");
        }

        private static void ExecuteListPageWarmStartTest(int i, string warmStartListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Warm start LoadTime");
            using IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5046/listPage");
            driver.Navigate().GoToUrl("http://localhost:5046/");

            var warmStartTestTracker = new Tracker();
            warmStartTestTracker.SetupAndStartMonitoring();

            warmStartTestTracker.StartTime();
            driver.Navigate().GoToUrl("http://localhost:5046/listPage");
            warmStartTestTracker.StopTime();

            var time = $"{warmStartTestTracker.GetTime()}ms";
            var memory = $"{warmStartTestTracker.GetMemoryUsage()}mb";
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

        private static long NavigateToUrl(IWebDriver driver, string url)
        {
            var initTracker = new Tracker();
            initTracker.SetupAndStartMonitoring();

            initTracker.StartTime();
            driver.Navigate().GoToUrl(url);
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