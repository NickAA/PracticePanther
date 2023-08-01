using PracticePanther.Models;

namespace PracticePanther.API.Database
{
    public class FakeDatabase
    {
        public static List<Client> clients = new List<Client>
            {
                new Client("Nicolas"),
                new Client("Penelope"),
                new Client("Andrew")
            };
    }
}
