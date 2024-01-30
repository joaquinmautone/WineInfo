using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.DAL.Infrastructure;
using WineInfo.DAL.Repositories;
using WineInfo.Entities;
using WineInfo.Services.Communication;

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

        public async Task<MesurementResponse> AddMesurementAsync(Mesurement mesurement)
        {
            mesurement = await repository.AddAsync(mesurement);
            SaveChanges();

            return new MesurementResponse(true, null, mesurement);
        }

        public async Task<Mesurement> GetMesurementByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id); 
        }

        public async Task<IEnumerable<Mesurement>> GetMesurementsAsync()
        {
            return await repository.GetAllAsync();
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
}
