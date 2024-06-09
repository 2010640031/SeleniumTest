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
                    ExecuteBaselineTest();
                    ExecuteTests(i, testCase);
                }
            }

            Console.WriteLine("=/=/=/=/=/=/=/=/=END=/=/=/=/=/=/=/=/=");
        }

        private static void ExecuteBaselineTest()
        {
            using IWebDriver driver = new ChromeDriver();
            var toggleTestTracker = new Tracker();
            toggleTestTracker.SetupAndStartMonitoring();
            
            var memory = $"{toggleTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{toggleTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{processorTime};{memory};";
            
            var warmStartListPage1000FilePath = Path.Combine(BaseDirectory, "baseline.csv");

            AppendDataToFile(warmStartListPage1000FilePath, dataLine);
        }

        private static void InitializeFiles(string resultsDirectory)
        {
            if (!Directory.Exists(resultsDirectory))
            {
                Directory.CreateDirectory(resultsDirectory);
            }

            InitializeFile("primeFile.csv", Header, resultsDirectory);
            InitializeFile("baseline.csv", Header, resultsDirectory);
            
            InitializeFile("warmStartListPage100File.csv", Header, resultsDirectory);
            InitializeFile("warmStartListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("warmStartListPage10000File.csv", Header, resultsDirectory);
            
            InitializeFile("setColorListPage100File.csv", Header, resultsDirectory);
            InitializeFile("setColorListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("setColorListPage10000File.csv", Header, resultsDirectory);
            
            InitializeFile("filterListPage100File.csv", Header, resultsDirectory);
            InitializeFile("filterListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("filterListPage10000File.csv", Header, resultsDirectory);
            
            InitializeFile("add100CarsListPage100File.csv", Header, resultsDirectory);
            InitializeFile("add1000CarsListPage100File.csv", Header, resultsDirectory);
            InitializeFile("add10000CarsListPage100File.csv", Header, resultsDirectory);
            
            InitializeFile("add100CarsListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("add1000CarsListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("add10000CarsListPage1000File.csv", Header, resultsDirectory);
            
            InitializeFile("add100CarsListPage10000File.csv", Header, resultsDirectory);
            InitializeFile("add1000CarsListPage10000File.csv", Header, resultsDirectory);
            InitializeFile("add10000CarsListPage10000File.csv", Header, resultsDirectory);
            
            InitializeFile("deleteAdditionalCarsListPage100File.csv", Header, resultsDirectory);
            InitializeFile("deleteAdditionalCarsListPage1000File.csv", Header, resultsDirectory);
            InitializeFile("deleteAdditionalCarsListPage10000File.csv", Header, resultsDirectory);

            InitializeFile("initialLoadTimesFile.csv", "Iteration;primePage;ListPage100;ListPage1000;ListPage10000;", resultsDirectory);
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
            
            var setColorListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "setColorListPage100File.csv");
            var setColorListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "setColorListPage1000File.csv");
            var setColorListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "setColorListPage10000File.csv");
            
            var filterListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "filterListPage100File.csv");
            var filterListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "filterListPage1000File.csv");
            var filterListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "filterListPage10000File.csv");
            
            var add100CarsListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "add100CarsListPage100File.csv");
            var add1000CarsListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "add1000CarsListPage100File.csv");
            var add10000CarsListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "add10000CarsListPage100File.csv");
            
            var add100CarsListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "add100CarsListPage1000File.csv");
            var add1000CarsListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "add1000CarsListPage1000File.csv");
            var add10000CarsListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "add10000CarsListPage1000File.csv");
            
            var add100CarsListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "add100CarsListPage10000File.csv");
            var add1000CarsListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "add1000CarsListPage10000File.csv");
            var add10000CarsListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "add10000CarsListPage10000File.csv");
            
            var deleteAddedCarsListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "deleteAdditionalCarsListPage100File.csv");
            var deleteAddedCarsListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "deleteAdditionalCarsListPage1000File.csv");
            var deleteAddedCarsListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "deleteAdditionalCarsListPage10000File.csv");
            
            var warmStartListPage100FilePath = Path.Combine(testCase.ResultsDirectory, "warmStartListPage100File.csv");
            var warmStartListPage1000FilePath = Path.Combine(testCase.ResultsDirectory, "warmStartListPage1000File.csv");
            var warmStartListPage10000FilePath = Path.Combine(testCase.ResultsDirectory, "warmStartListPage10000File.csv");

            
            var initialLoadTimesFilePath = Path.Combine(testCase.ResultsDirectory, "InitialLoadTimesFile.csv");
            
            long? initialLoadTimeprimePage; 
            
            long? initialLoadTimeListPage100; 
            long? initialLoadTimeListPage1000; 
            long? initialLoadTimeListPage10000; 
            
            initialLoadTimeListPage100 =
                 ExecuteListPageModificationTest(iteration, setColorListPage100FilePath, filterListPage100FilePath, 
                     add100CarsListPage100FilePath, add1000CarsListPage100FilePath,add10000CarsListPage100FilePath,deleteAddedCarsListPage100FilePath,
                     testCase.Url + "/ListPage100" );
            initialLoadTimeListPage1000 =
                ExecuteListPageModificationTest(iteration, setColorListPage1000FilePath, filterListPage1000FilePath,
                    add100CarsListPage1000FilePath,add1000CarsListPage1000FilePath,add10000CarsListPage1000FilePath,deleteAddedCarsListPage1000FilePath,
                    testCase.Url + "/ListPage1000");
            initialLoadTimeListPage10000 =
                ExecuteListPageModificationTest(iteration, setColorListPage10000FilePath, filterListPage10000FilePath,
                    add100CarsListPage10000FilePath,add1000CarsListPage10000FilePath,add10000CarsListPage10000FilePath,deleteAddedCarsListPage10000FilePath,
                    testCase.Url + "/ListPage10000");
            
             initialLoadTimeprimePage = ExecutePrimeTest(iteration, primeFilePath, testCase.Url);

            ExecuteListPageWarmStartTest(iteration, warmStartListPage100FilePath, testCase.Url + "/ListPage100");
            
            ExecuteListPageWarmStartTest(iteration, warmStartListPage1000FilePath, testCase.Url + "/ListPage1000");
            
            ExecuteListPageWarmStartTest(iteration, warmStartListPage10000FilePath, testCase.Url + "/ListPage10000");

            var dataLine = $"{iteration};{initialLoadTimeprimePage}ms;{initialLoadTimeListPage100}ms;{initialLoadTimeListPage1000}ms;{initialLoadTimeListPage10000}ms;";
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
            string filterListPageFilePath,string addCars100ListPageFilePath,string addCars1000ListPageFilePath,string addCars10000ListPageFilePath,string deleteAddedCarsListPageFilePath, string url)
        {
            using IWebDriver driver = new ChromeDriver();
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for List Page");

            var initialLoadTime = NavigateToUrl(driver, url  , "togglebrandbtn");

            ExecuteToggle(driver, i, filterListPageFilePath);
            ExecuteColorSet(driver, i, setColorListPageFilePath);
            AddCars(driver, i, addCars100ListPageFilePath, addCars1000ListPageFilePath, addCars10000ListPageFilePath);
            DeleteCars(driver, i, deleteAddedCarsListPageFilePath);
            return initialLoadTime;
        }

        private static void DeleteCars(IWebDriver driver, int i, string deleteAddedCarsListPageFilePath)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Delete Cars");

            var deleteAddedCarsButton = driver.FindElement(By.Id("deletealladdedcarsbtn"));

            var deleteAddedCarsTestTracker = new Tracker();
            deleteAddedCarsTestTracker.SetupAndStartMonitoring();

            deleteAddedCarsTestTracker.StartTime();
            deleteAddedCarsButton.Click();
            deleteAddedCarsTestTracker.StopTime();

            var time = $"{deleteAddedCarsTestTracker.GetTime()}ms";
            var memory = $"{deleteAddedCarsTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{deleteAddedCarsTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(deleteAddedCarsListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {deleteAddedCarsTestTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {deleteAddedCarsTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {deleteAddedCarsTestTracker.GetCpuUsage()} ms");
           
        }

        private static void AddCars(IWebDriver driver, int i, string add100CarsListPageFilePath, string add1000CarsListPageFilePath, string add10000CarsListPageFilePath)
        {
            ExecuteAddCars(driver, i, add100CarsListPageFilePath,"getmorecars100btn");
            ExecuteAddCars(driver, i, add1000CarsListPageFilePath,"getmorecars1000btn");
            ExecuteAddCars(driver, i, add10000CarsListPageFilePath,"getmorecars10000btn");
        }

        private static void ExecuteAddCars(IWebDriver driver, int i, string addCarsListPageFilePath, string buttonId)
        {
            Console.WriteLine($"-------------------------------------");
            Console.WriteLine("Starting Test for Add Cars");

            var addCarsButton = driver.FindElement(By.Id(buttonId));

            var add100CarsTestTracker = new Tracker();
            add100CarsTestTracker.SetupAndStartMonitoring();

            add100CarsTestTracker.StartTime();
            addCarsButton.Click();
            add100CarsTestTracker.StopTime();

            var time = $"{add100CarsTestTracker.GetTime()}ms";
            var memory = $"{add100CarsTestTracker.GetMemoryUsage() / 1024 / 1024}mb";
            var processorTime = $"{add100CarsTestTracker.GetCpuUsage()}ms";

            var dataLine = $"{i};{time};{memory};{processorTime};";

            AppendDataToFile(addCarsListPageFilePath, dataLine);

            Console.WriteLine("-------------------------------------");
            Console.WriteLine($"  Time taken                : {add100CarsTestTracker.GetTime()} ms");
            Console.WriteLine($"  Physical memory usage     : {add100CarsTestTracker.GetMemoryUsage() / 1024 / 1024} MB");
            Console.WriteLine($"  Total processor time      : {add100CarsTestTracker.GetCpuUsage()} ms");
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
            driver.Navigate().GoToUrl(url);
            new WebDriverWait(driver, TimeSpan.FromSeconds(200)).Until(
                ExpectedConditions.ElementToBeClickable(By.Id("donediv")));
            driver.Navigate().GoToUrl("about:blank");

            var warmStartTestTracker = new Tracker();
            warmStartTestTracker.SetupAndStartMonitoring();

            warmStartTestTracker.StartTime();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.history.go(-1)");
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(200)).Until(
                    ExpectedConditions.ElementToBeClickable(By.Id("donediv")));
            }
            catch (NoSuchElementException e)
            {
            }
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