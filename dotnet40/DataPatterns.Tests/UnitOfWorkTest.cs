using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataPatterns.Socle;
using DataPatterns.Tests.Mocks;

namespace DataPatterns.Tests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void Commit_ShouldCallSaveChangesOnContext()
        {
            // Arrange
            var mockContext = new MockObjectContext();
            var unitOfWork = new UnitOfWork(mockContext);

            // Act
            unitOfWork.Commit();

            // Assert
            Assert.IsTrue(mockContext.SaveChangesCalled);
        }

        [TestMethod]
        public void Dispose_ShouldCallDisposeOnContext()
        {
            // Arrange
            var mockContext = new MockObjectContext();
            var unitOfWork = new UnitOfWork(mockContext);

            // Act
            unitOfWork.Dispose();

            // Assert
            Assert.IsTrue(mockContext.DisposeCalled);
        }
    }
}
