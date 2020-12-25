using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void GetByQuery_WithIsbn_CallsGetByQuery()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            bookRepositoryStub.Setup(x => x.GetByIsbn(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "", "",0m) });

            bookRepositoryStub.Setup(x => x.GetByTitleOrAuthor(It.IsAny<string>()))
                .Returns(new[] { new Book(2, "", "", "", "",0m) });

            BookService bookService = new BookService(bookRepositoryStub.Object);

            var actual = bookService.GetByQuery("ISBN 1234-5678-90");

            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
            
        }
        [Fact]
        public void GetByQuery_WithAuthor_CallsGetByTitleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            bookRepositoryStub.Setup(x => x.GetByIsbn(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "", "",0m) });

            bookRepositoryStub.Setup(x => x.GetByTitleOrAuthor(It.IsAny<string>()))
                .Returns(new[] { new Book(2, "", "", "","",0m) });

            BookService bookService = new BookService(bookRepositoryStub.Object);

            var actual = bookService.GetByQuery("1234-5678-90");

            Assert.Collection(actual, book => Assert.Equal(2, book.Id));

        }
    }
}
