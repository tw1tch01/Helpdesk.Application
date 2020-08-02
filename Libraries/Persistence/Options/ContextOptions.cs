namespace Helpdesk.Persistence.Options
{
    public class ContextOptions
    {
        public MySqlOptions TicketContext { get; set; }
        public MySqlOptions UserContext { get; set; }
    }
}