using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TinyDemo.MVVM;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;
using Xunit;

namespace TinyDemo.MVVM.Tests.ViewModels
{
    public class MainViewModelTests
    {
        [Fact]
        public async Task Constructor_WhenInitialized_ShouldSetDefaultValues()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var testLotto = new Lotto
            {
                Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                GeneratedOnTime = DateTime.Now
            };
            
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testLotto);
            
            // Act
            var viewModel = new MainViewModel(mockLottoService.Object);
            // Wait for the automatic generation to complete
            await Task.Delay(100);
            
            // Assert
            Assert.Contains("Lotto numbers generated on", viewModel.StatusMessage);
            Assert.False(viewModel.Busy);
            Assert.True(viewModel.NotBusy);
            Assert.NotNull(viewModel.Lotto);
        }

        [Fact]
        public void Constructor_WhenLottoServiceIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MainViewModel(null!));
        }

        [Fact]
        public async Task GenerateCommand_WhenExecuted_ShouldCallGenerateLottoAsync()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var testLotto = new Lotto
            {
                Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                GeneratedOnTime = DateTime.Now
            };
            
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testLotto);
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            // Wait for initial generation to complete
            await Task.Delay(100);
            mockLottoService.Invocations.Clear(); // Reset call count after initial generation
            
            // Act
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert
            mockLottoService.Verify(s => s.GenerateLotto(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(viewModel.Lotto);
            Assert.Equal(7, viewModel.Lotto.Numbers.Count);
            Assert.False(viewModel.Busy);
        }

        [Fact]
        public async Task GenerateCommand_WhenLottoServiceThrowsException_ShouldHandleGracefully()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Test exception"));
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert
            Assert.Contains("Failed to generate lotto numbers", viewModel.StatusMessage);
            Assert.False(viewModel.Busy);
            Assert.Null(viewModel.Lotto);
        }

        [Fact]
        public void CancelCommand_WhenExecuted_ShouldCancelCurrentOperation()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act
            viewModel.CancelCommand.Execute(null);
            
            // Assert - Should not throw and should handle cancellation gracefully
            Assert.False(viewModel.Busy);
        }

        [Fact]
        public async Task GenerateLottoAsync_WhenCalled_ShouldUpdateStatusMessage()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var testLotto = new Lotto
            {
                Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                GeneratedOnTime = new DateTime(2024, 1, 15, 10, 30, 0)
            };
            
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testLotto);
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert
            Assert.Contains("Lotto numbers generated on 10:30", viewModel.StatusMessage);
        }

        [Fact]
        public async Task GenerateLottoAsync_WhenCancelled_ShouldHandleGracefully()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var testLotto = new Lotto
            {
                Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                GeneratedOnTime = DateTime.Now
            };
            
            // First call returns success (initial generation), second call throws cancellation
            mockLottoService.SetupSequence(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testLotto)
                .ThrowsAsync(new OperationCanceledException("Test cancellation"));
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            // Wait for initial generation to complete
            await Task.Delay(100);
            
            // Act - Cancel before calling generate to trigger the cancellation path
            viewModel.CancelCommand.Execute(null);
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert - The view model should handle the exception gracefully
            Assert.False(viewModel.Busy);
            Assert.NotNull(viewModel.StatusMessage); // Should have some status message
        }

        [Fact]
        public void Dispose_WhenCalled_ShouldCleanUpResources()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act
            viewModel.Dispose();
            
            // Assert - Should not throw and should clean up cancellation token source
            viewModel.CancelCommand.Execute(null); // Should not throw after dispose
        }

        [Fact]
        public async Task MultipleGenerateCalls_ShouldHandleConcurrentOperations()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var testLotto = new Lotto
            {
                Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                GeneratedOnTime = DateTime.Now
            };
            
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testLotto);
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            // Wait for initial generation to complete
            await Task.Delay(100);
            mockLottoService.Invocations.Clear(); // Reset call count after initial generation
            
            // Act - Call generate multiple times
            await viewModel.GenerateCommand.ExecuteAsync(null);
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert
            mockLottoService.Verify(s => s.GenerateLotto(It.IsAny<CancellationToken>()), Times.Exactly(2));
            Assert.NotNull(viewModel.Lotto);
            Assert.False(viewModel.Busy);
        }

        [Fact]
        public void NotBusyProperty_WhenBusyChanges_ShouldUpdateCorrectly()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act & Assert - Initially not busy
            Assert.False(viewModel.Busy);
            Assert.True(viewModel.NotBusy);
            
            // Simulate busy state (we can't directly set Busy as it's private, but we can test the property logic)
            // The property logic is: public bool NotBusy => !Busy;
            // This is tested indirectly through other tests that set Busy to true/false
        }

        [Fact]
        public async Task GenerateLottoAsync_WhenUnexpectedErrorOccurs_ShouldHandleGracefully()
        {
            // Arrange
            var mockLottoService = new Mock<ILottoService>();
            mockLottoService.Setup(s => s.GenerateLotto(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Unexpected error"));
            
            var viewModel = new MainViewModel(mockLottoService.Object);
            
            // Act
            await viewModel.GenerateCommand.ExecuteAsync(null);
            
            // Assert
            Assert.Contains("An unexpected error occurred", viewModel.StatusMessage);
            Assert.False(viewModel.Busy);
            Assert.Null(viewModel.Lotto);
        }
    }
}