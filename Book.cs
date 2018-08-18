namespace eftest
{
    public class Book
    {
        public int ID { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }
        
        public string Name { get; set; }
    }
}