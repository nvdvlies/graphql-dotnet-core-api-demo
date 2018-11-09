namespace DemoGraphQL.Events
{
    public class MovieCreatedEvent
    {
        public MovieCreatedEvent(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
