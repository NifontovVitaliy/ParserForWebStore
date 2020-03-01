namespace ParserUI.Work
{
    public class Product
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return Name + "\n " + Price + "\n" + Description + "\n " + ImageUrl + "\n";
        }
    }
}
