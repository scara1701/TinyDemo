# TinyDemo - Multi-Platform Lotto Number Generator

A comprehensive demonstration project showing how to build the same application using different .NET UI frameworks, all sharing common business logic and data models.

## 📚 Project Overview

This solution demonstrates a **Lotto Number Generator** application implemented across multiple .NET UI frameworks:
- **MAUI** (Multi-platform App UI) - Cross-platform mobile/desktop
- **WPF** (Windows Presentation Foundation) - Desktop
- **WinForms** (Windows Forms) - Desktop
- **WebAPI** - Backend REST API

All clients share the same business logic, service interface, and data models through a shared library.

## 🏗️ Solution Architecture

```
TinyDemo/
├── TinyDemo.SharedLib/          # Shared entities and interfaces
├── TinyDemo.WebAPI/             # REST API backend
├── TinyDemo.ClientLib/          # Client-side services
├── TinyDemo.ClientLib.Tests/    # Unit tests for ClientLib
├── TinyDemo.MVVM/               # ViewModel layer
├── TinyDemo.MauiClient/         # MAUI cross-platform client
├── TinyDemo.WPFClient/          # WPF desktop client
└── TinyDemo.WFClient/           # WinForms desktop client
```

## 🎯 Key Features

- **Single Business Logic**: All clients use the same `ILottoService` interface
- **Shared Data Models**: Common `Lotto` entity across all projects
- **MVVM Pattern**: Consistent ViewModel implementation using CommunityToolkit.MVVM
- **Dependency Injection**: Used throughout all project types
- **REST API**: Modern ASP.NET Core Web API
- **Cross-Platform**: MAUI client targets Android, iOS, macOS, and Windows

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (17.14 or later) with:
  - MAUI workload
  - ASP.NET Core workload
  - Desktop development workload
  - xUnit test runner (optional, for test projects)

### Running Tests

To run the unit tests:

```bash
cd TinyDemo.ClientLib.Tests
_dotnet test
```

This will execute all tests in the test project and provide a summary of passed/failed tests.

To run tests with detailed output:

```bash
_dotnet test -v detailed
```

To run tests and generate code coverage (requires additional tools):

```bash
_dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

### Running the Application

#### 1. Start the WebAPI

```bash
cd TinyDemo.WebAPI
_dotnet run
```

The API will start on `https://localhost:7049` with Swagger UI available at:
👉 `https://localhost:7049/swashbuckle/index.html`

#### 2. Run a Client Application

Choose one of the following:

**MAUI Client (Cross-platform)**
```bash
cd TinyDemo.MauiClient
_dotnet build
```
Then run for your target platform:
- Android: Use Visual Studio Android emulator
- iOS: Use Visual Studio iOS simulator
- Windows: `_dotnet run -t:run -f net9.0-windows10.0.19041.0`
- macOS: `_dotnet run -t:run -f net9.0-maccatalyst`

**WPF Client**
```bash
cd TinyDemo.WPFClient
_dotnet run
```

**WinForms Client**
```bash
cd TinyDemo.WFClient
_dotnet run
```

## 📦 Project Details

### TinyDemo.SharedLib

Shared library containing core entities and interfaces:

**Entities:**
- `Lotto`: Contains `ObservableCollection<int> Numbers` and `GeneratedOnTime`

**Services:**
- `ILottoService`: Interface with `GenerateLotto()` method

### TinyDemo.ClientLib.Tests

Unit test project for ClientLib using xUnit:

**Test Coverage:**
- `LottoServiceTests`: Comprehensive tests for `LottoService`
  - Success scenarios with valid API responses
  - Error handling for empty responses
  - Error handling for API errors (500, etc.)
  - Invalid JSON handling
  - Null response handling
  - Correct deserialization verification
  - Configuration validation (API URL, JSON options)

**Testing Technologies:**
- **xUnit**: Test framework for .NET
- **Moq**: Mocking framework for HTTP client
- **Microsoft.NET.Test.Sdk**: Test execution infrastructure
- **Reflection**: For testing private fields

**Test Statistics:**
- 9 test methods covering all major scenarios
- 100% coverage of `LottoService` public methods
- Tests for both happy path and error conditions
- Isolated unit tests with no external dependencies

**How to Run Tests:**

```bash
# Run all tests
cd TinyDemo.ClientLib.Tests
_dotnet test

# Run with detailed output
_dotnet test -v detailed

# Run specific test class
_dotnet test --filter "LottoServiceTests"

# Run specific test method
_dotnet test --filter "GenerateLotto_WhenApiReturnsSuccess_ShouldReturnLottoWithNumbers"
```

### TinyDemo.WebAPI

ASP.NET Core Web API providing REST endpoints:

**Endpoint:**
- `GET /api/Lotto` - Returns 7 random lotto numbers (1-45)

**Features:**
- Swagger/OpenAPI documentation
- JSON serialization
- Dependency Injection
- Error handling

**Implementation:**
- Generates 7 unique random numbers from 1 to 45
- Simulates processing with 250ms delay
- Returns timestamp with generated numbers

### TinyDemo.ClientLib

Client-side service implementation:

**Services:**
- `LottoService`: Implements `ILottoService`
  - Calls WebAPI endpoint
  - Handles HTTP communication
  - JSON deserialization
  - Error handling
  - Comprehensive unit test coverage (see Testing section)

### TinyDemo.MVVM

ViewModel layer using CommunityToolkit.MVVM:

**ViewModels:**
- `MainViewModel`:
  - `Lotto` property (bound to UI)
  - `StatusMessage` property
  - `Busy` property (for loading state)
  - `GenerateCommand` (RelayCommand)
  - `GenerateLottoAsync()` method

### TinyDemo.MauiClient

MAUI cross-platform application:

**Features:**
- Targets: Android, iOS, macOS, Windows
- Custom `NumberControl` (ContentView)
- XAML-based UI with data binding
- Dependency Injection setup

**Custom Control:**
- `NumberControl.xaml` - Displays individual lotto numbers
- Bindable `Number` property
- Used 7 times in MainPage (6 main + 1 bonus)

### TinyDemo.WPFClient

WPF desktop application:

**Features:**
- XAML-based UI
- MVVM pattern
- Dependency Injection
- Modern WPF styling

### TinyDemo.WFClient

Windows Forms desktop application:

**Features:**
- Classic WinForms UI
- Uses same service interface
- Simpler UI implementation

## 🎨 UI Examples

### MAUI Client
```xml
<StackLayout Orientation="Horizontal">
    <controls:NumberControl Number="{Binding Lotto.Numbers[0]}" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[1]}" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[2]}" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[3]}" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[4]}" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[5]}" />
    <Label Text="+" FontSize="28" FontAttributes="Bold" />
    <controls:NumberControl Number="{Binding Lotto.Numbers[6]}" />
</StackLayout>
<Button Text="Generate new Lotto numbers" Command="{Binding GenerateCommand}" />
```

### ViewModel Code
```csharp
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    Lotto lotto;

    [ObservableProperty]
    string statusMessage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotBusy))]
    bool busy;

    public bool NotBusy => !Busy;

    [RelayCommand]
    private async void Generate()
    {
        await GenerateLottoAsync();
    }

    private async Task GenerateLottoAsync()
    {
        Busy = true;
        StatusMessage = "Generating lotto numbers. Please wait....";
        Lotto = await _lottoService.GenerateLotto();
        StatusMessage = $"Lotto numbers generated on {Lotto.GeneratedOnTime.ToShortTimeString()}";
        Busy = false;
    }
}
```

## 🔧 Technical Implementation

### Dependency Injection Setup

**WebAPI (Program.cs):**
```csharp
builder.Services.AddScoped<ILottoService, LottoService>();
```

**MAUI Client (App.xaml.cs):**
```csharp
services.AddSingleton<ILottoService, LottoService>();
services.AddSingleton<HttpClient>();
services.AddTransient<MainViewModel>();
services.AddTransient<MainPage>();
services.AddSingleton<AppShell>();
```

### API Communication

**Client Service:**
```csharp
public async Task<Lotto> GenerateLotto()
{
    HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<Lotto>(responseBody, _options);
}
```

### Lotto Generation Algorithm

```csharp
public async Task<Lotto> GenerateLotto()
{
    List<int> numbers = Enumerable.Range(1, 45).ToList();
    Random random = new Random();
    List<int> selectedNumbers = numbers
        .OrderBy(x => random.Next())
        .Take(7)
        .ToList();
    
    await Task.Delay(250); // Simulate processing
    
    return new Lotto
    {
        GeneratedOnTime = DateTime.Now,
        Numbers = new ObservableCollection<int>(selectedNumbers)
    };
}
```

## 🧪 Testing

### Test Project Structure

The solution includes a comprehensive unit test project `TinyDemo.ClientLib.Tests` that provides test coverage for the `LottoService` class.

### Test Organization

**Location:** `TinyDemo.ClientLib.Tests/Services/LottoServiceTests.cs`

**Test Categories:**

1. **Success Scenarios (2 tests)**
   - Valid API response handling
   - Data deserialization verification

2. **Error Handling (4 tests)**
   - Empty response body
   - API error status codes
   - Invalid JSON format
   - Null response content

3. **Configuration Tests (2 tests)**
   - API URL validation
   - JSON serialization options

4. **HTTP Error Tests (1 test)**
   - Error status code handling

### Mocking Strategy

Tests use **Moq** to mock the `HttpMessageHandler` to:
- Isolate the service from actual network dependencies
- Control API responses for testing different scenarios
- Avoid external dependencies in unit tests
- Improve test reliability (no network flakiness)
- Speed up test execution (no actual HTTP calls)

### Test Execution

```bash
# Run all tests
_dotnet test

# Run with detailed output
_dotnet test -v detailed

# Run specific test class
_dotnet test --filter "LottoServiceTests"

# Run specific test method
_dotnet test --filter "GenerateLotto_WhenApiReturnsSuccess_ShouldReturnLottoWithNumbers"
```

### Test Results Interpretation

When tests are run, you should see output similar to:

```
TinyDemo.ClientLib.Tests
  LottoServiceTests
    ✓ GenerateLotto_WhenApiReturnsSuccess_ShouldReturnLottoWithNumbers [123ms]
    ✓ GenerateLotto_WhenApiReturnsEmptyBody_ShouldThrowException [45ms]
    ✓ GenerateLotto_WhenApiReturnsErrorStatus_ShouldThrowException [34ms]
    ✓ GenerateLotto_WhenApiReturnsInvalidJson_ShouldThrowException [23ms]
    ✓ LottoService_WhenInitialized_ShouldHaveCorrectApiUrl [12ms]
    ✓ LottoService_WhenInitialized_ShouldHaveCorrectJsonOptions [15ms]
    ✓ GenerateLotto_WhenApiReturnsNullResponse_ShouldThrowException [28ms]
    ✓ GenerateLotto_WithValidResponse_ShouldDeserializeCorrectly [56ms]

Test Run Successful.
Total: 9 tests, all passed
```

### Benefits of Testing

1. **Quality Assurance**: Catches bugs early in development
2. **Regression Prevention**: Ensures existing functionality doesn't break
3. **Documentation**: Tests serve as executable documentation
4. **Confidence**: Enables safe refactoring
5. **Maintainability**: Makes code easier to understand and modify
6. **CI/CD Ready**: Can be integrated into build pipelines
7. **Developer Productivity**: Fast feedback cycle

## 🎓 Learning Objectives

This project demonstrates:

1. **Cross-Platform Development**: Building MAUI apps for multiple platforms
2. **UI Framework Comparison**: WPF vs WinForms vs MAUI
3. **MVVM Pattern**: Proper implementation with CommunityToolkit.MVVM
4. **API Design**: RESTful service design with ASP.NET Core
5. **Code Sharing**: Sharing business logic across different UI frameworks
6. **Dependency Injection**: Using DI in different .NET project types
7. **Custom Controls**: Creating reusable UI components
8. **Data Binding**: Binding data to UI in different frameworks
9. **Asynchronous Programming**: Async/await patterns
10. **Modern .NET**: Using .NET 8 and .NET 9 features
11. **Unit Testing**: Comprehensive test coverage with xUnit and Moq

## 📝 Notes

- The WebAPI must be running before clients can fetch lotto numbers
- All clients call the same API endpoint
- The MAUI client uses `https://localhost:7049` for local development
- For production, update the API URL in `LottoService`
- The project uses camelCase JSON naming policy

## 🔮 Future Enhancements

Potential improvements:
- Add user authentication
- Store generated numbers in a database
- Add statistics and history
- Implement caching
- Add integration tests for API endpoints
- Add UI tests for MAUI/WPF/WinForms clients
- Add more unit tests for ViewModel layer
- Add unit tests for WebAPI layer
- Support for different lotto games (5/35, 6/49, etc.)
- Mobile-specific features (notifications, offline mode)
- CI/CD pipeline with automated testing

## 📄 License

This project is for demonstration purposes only.

## 🤝 Contributing

This is a demo project. Feel free to fork and modify for your own learning purposes.

---

**Created as a learning demonstration project**
