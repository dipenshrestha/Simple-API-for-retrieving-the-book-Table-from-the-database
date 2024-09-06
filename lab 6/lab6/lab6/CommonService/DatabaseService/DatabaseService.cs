using lab6.Models;
using Npgsql;
using System.Data;

namespace lab6.CommonService.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService() 
        {
            this._connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=sekaiwiz;Database=labFive";
        }

        public string AddBook(Book book)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.Parameters.AddWithValue("@book_id", book.bookId);
            comm.Parameters.AddWithValue("@bookName", book.bookName);
            comm.Parameters.AddWithValue("@authorName", book.authorName);
            comm.Parameters.AddWithValue("@bookPrice", book.bookPrice);
            comm.CommandText = "insert into \"books\" (book_id,book_name,athr_name,book_price) values(@book_id,@bookName,@authorName,@bookPrice)";
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();

            return "Book Added Successfully";
        }

        public Book GetBookById(int id)
        {
            Book book = null;

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (NpgsqlCommand comm = new NpgsqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT * FROM \"books\" WHERE book_id = @Id";
                    comm.Parameters.AddWithValue("@Id", id);

                    using (NpgsqlDataReader dr = comm.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            book = new Book
                            {
                                bookId = Convert.ToInt32(dr["book_id"]),
                                bookName = dr["book_name"].ToString(),
                                authorName = dr["athr_name"].ToString(),
                                bookPrice = Convert.ToInt32(dr["book_price"])
                            };
                        }
                    }
                }
            }

            return book;
        }

        public IReadOnlyList<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select * from \"books\"";
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                Book book = new Book()
                {
                    bookId = Convert.ToInt32(dr["book_id"]),
                    bookName = dr["book_name"].ToString(),
                    authorName = dr["athr_name"].ToString(),
                    bookPrice = Convert.ToInt32(dr["book_price"])
                };
                books.Add(book);
            }
            comm.Dispose();
            conn.Close();
            return books;
        }
    }
}
