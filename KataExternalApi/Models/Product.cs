namespace KataExternalApi.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Rating Rating { get; set; }
    }

    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }

}