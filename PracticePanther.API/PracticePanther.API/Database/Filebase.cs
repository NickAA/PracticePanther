using Newtonsoft.Json;
using PracticePanther.API.Controllers;
using PracticePanther.API.EC;
using PracticePanther.Models;
using System.Text;

namespace PracticePanther.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _clientRoot;
        private string _projectRoot;
        private static Filebase? _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "C:\\temp";
            _clientRoot = $"{_root}\\Clients";
            _projectRoot = $"{_root}\\Projects";
        }

        private int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;

        public Client Add(Client c)
        {
            //set up a new Id if one doesn't already exist
            c.Id = LastClientId + 1;

            var path = $"{_clientRoot}\\{c.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
                //blow it up
                File.Delete(path);

            //write the file
            //File.WriteAllText(path, JsonConvert.SerializeObject(c));

            using (var fw = new FileStream(path, FileMode.Create))
                fw.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(c)));

                //return the client, which now has an id
                return c;
        }

        public Client Update(Client c)
        {
            var path = $"{_clientRoot}\\{c.Id}.json";

            if(File.Exists(path))
            {
                File.Delete(path);

                using (var fw = new FileStream(path, FileMode.Create))
                    fw.Write(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(c)));
            }

            return c;
        }

        public List<Client> Clients
        {
            get
            {
                var root = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                foreach(var todoFile in root.GetFiles())
                {
                    var todo = JsonConvert.DeserializeObject<Client>(File.ReadAllText(todoFile.FullName));

                    if (todo != null)
                        _clients.Add(todo);
                }
                return _clients;
            }
        }

        //public List<Appointment> Appointments
        //{
        //    get
        //    {
        //        var root = new DirectoryInfo(_appointmentRoot);
        //        var _apps = new List<Appointment>();
        //        foreach (var appFile in root.GetFiles())
        //        {
        //            var app = JsonConvert.DeserializeObject<Appointment>(File.ReadAllText(appFile.FullName));
        //            _apps.Add(app);
        //        }
        //        return _apps;
        //    }
        //}

        public bool Delete(int id)
        {
            var path = $"{_clientRoot}\\{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }


            return true;
        }
    }
}
