using MySerial.Model;
using MySerial.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerial.Repository.Aggregator
{
    public class RepositoryAggregator
    {
        private List<ISerialRepository> m_Repositories;

        public RepositoryAggregator()
        {
            m_Repositories = new List<ISerialRepository>();
        }

        public IReadOnlyList<ISerialRepository> Repositories => m_Repositories;

        public void AddRepository(ISerialRepository repository)
        {
            m_Repositories.Add(repository);
        }

        public void RemoveRepository(ISerialRepository repository)
        {
            m_Repositories.Remove(repository);
        }

        public async Task<IEnumerable<Serial>> GetSerials(string filter = null)
        {
            return Enumerable.SelectMany(
                await Task.WhenAll(m_Repositories.Select(repo => repo.GetSerials(filter))),
                x => x);
        }
    }
}
