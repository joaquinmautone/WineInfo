using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.Entities;
using WineInfo.Services.Communication;

namespace WineInfo.Services
{
    public interface IMesurementService
    {
        Task<IEnumerable<Mesurement>> GetMesurementsAsync();
        Task<MesurementResponse> AddMesurementAsync(Mesurement mesurement);
        Task<Mesurement> GetMesurementByIdAsync(int id);
    }
}
