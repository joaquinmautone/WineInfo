using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.DAL.Repositories;
using WineInfo.Entities;

namespace WineInfo.Services
{
    public class MesurementService : IMesurementService
    {
        private IMesurementRepository repository;
        private IUnitOfWork unitOfWork;

        public MesurementService(IMesurementRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Mesurement AddMesurement(Mesurement mesurement)
        {
            repository.Add(mesurement);
            SaveChanges();

            return mesurement;
        }

        public void DeleteMesurement(int id)
        {
            var equipo = repository.GetById(id);
            repository.Delete(equipo);

            SaveChanges(); 
        }

        public Mesurement GetMesurementById(int id)
        {
            return repository.GetById(id); 
        }

        public async Task<IEnumerable<Mesurement>> GetMesurementsAsync()
        {
            return await repository.GetAllAsync();
        }

        public Mesurement UpdateMesurement(Mesurement mesurement)
        {
            repository.Update(mesurement);
            SaveChanges();

            return mesurement;
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
}
