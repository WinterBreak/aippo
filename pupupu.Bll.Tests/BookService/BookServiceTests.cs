using Moq;
using pupupu.Models.DAL;
using pupupu.Queries;
using pupupu.Repositories.Interfaces;
using pupupu.Services;
using pupupu.Services.Interfaces;


namespace pupupu.Bll.Tests;

[TestFixture]
public class BookServiceTests
{
    const int NON_EXISTING_BOOK_ID = -1;
    const int CORRECT_BOOK_ID = 1;
    private Mock<IBookServiceInterface> _mockBookService;
    private Mock<IBookRepository> _mockBookRepository;
    private IBookServiceInterface _service;

    [SetUp]
    public void SetUp()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockBookService = new Mock<IBookServiceInterface>();
        _service = new BookService(_mockBookRepository.Object);
        _mockBookService
            .Setup(service => service.GetBookById(It.IsAny<int>()))
            .Returns((int bookId) => _service.GetBookById(bookId));
    }

    [Test]
    public void GetBooks_ShouldReturnAllBooks()
    {
        var bookAuthor = new Author {Id = 1, Name = "name"};
        var bookList = new List<Book>
        {
            new Book {Id = 1, Title = "book1", Description = "blablabla", AuthorId = bookAuthor.Id, Author = bookAuthor},
            new Book {Id = 2, Title = "book2", Description = "pupupu", AuthorId = bookAuthor.Id, Author = bookAuthor},
        };
        
        _mockBookRepository
            .Setup(repos => repos.GetAllBooks())
            .Returns(bookList.AsQueryable());
        
        var result = _service.GetBooks();
        Assert.That(result, Is.EquivalentTo(bookList));
    }

    [Test]
    public void GetBooksByAuthorId_ShouldReturnBooksByAuthorId()
    {
        var bookAuthor1 = new Author {Id = 1, Name = "name1"};
        var bookAuthor2 = new Author {Id = 2, Name = "name2"};
        var bookList = new List<Book>
        {
            new Book {Id = 1, Title = "book1", Description = "blablabla", AuthorId = bookAuthor1.Id, Author = bookAuthor1},
            new Book {Id = 2, Title = "book2", Description = "pupupu", AuthorId = bookAuthor1.Id, Author = bookAuthor1},
        };
        
        _mockBookRepository
            .Setup(repos => repos.GetBooksByAuthorId(bookAuthor1.Id))
            .Returns(bookList.AsQueryable());
        
        var result = _service.GetBooksByAuthorId(bookAuthor1.Id);
        Assert.That(result, Is.EquivalentTo(bookList));
    }

    [Test]
    public void GetBooksByAuthorId_NonExistingBooks_ShouldReturnEmptyList()
    {
        var bookAuthor1 = new Author {Id = 1, Name = "name1"};
        var bookAuthor2 = new Author {Id = 2, Name = "name2"};
        var bookList = new List<Book>
        {
            new Book {Id = 1, Title = "book1", Description = "blablabla", AuthorId = bookAuthor2.Id, Author = bookAuthor2},
            new Book {Id = 2, Title = "book2", Description = "pupupu", AuthorId = bookAuthor2.Id, Author = bookAuthor2},
        };
        
        _mockBookRepository
            .Setup(repos => repos.GetBooksByAuthorId(bookAuthor1.Id))
            .Returns(bookList.AsQueryable());
        
        var result = _service.GetBooksByAuthorId(bookAuthor1.Id);
        Assert.That(result, Is.EquivalentTo(bookList));
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetBooks_ShouldReturnEmptyListIfNoBooks()
    {
        var bookList = new List<Book>{};
        
        _mockBookRepository
            .Setup(repos => repos.GetAllBooks())
            .Returns(bookList.AsQueryable());
        
        var result = _service.GetBooks();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetBookById_CorrectId_ReturnsBook()
    {
        var expectedBook = new Book
        {
            Id = CORRECT_BOOK_ID,
            Title = "Книга"
        };
        
        _mockBookRepository
            .Setup(repos => repos.GetBookById(CORRECT_BOOK_ID))
            .Returns(expectedBook);
        
        var result = _mockBookService.Object.GetBookById(CORRECT_BOOK_ID);
        
        Assert.That(result.Id, Is.EqualTo(expectedBook.Id));
        Assert.That(result.Title, Is.EqualTo(expectedBook.Title));
    }

    [Test]
    public void GetBookById_NonExistingId_ReturnsNull()
    {
        _mockBookRepository
            .Setup(repos => repos.GetBookById(NON_EXISTING_BOOK_ID))
            .Returns((Book)null);

        var result = _mockBookService.Object.GetBookById(NON_EXISTING_BOOK_ID);

        Assert.That(result, Is.Null);
    }
}