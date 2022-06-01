using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }
        //Redis ile bağlantı kurmak için kullancağımız sınıf
        private ConnectionMultiplexer _connectionMultiplexer;

        //public void Connect()
        //{
        //    _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        //}
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");


        //10 15 tane redis te default gele nveritabanları var. Bunlar test o bu şu için kolay olsun diye ayrılmış bunlardan birini seçiyoruz.
        public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
    }
}
