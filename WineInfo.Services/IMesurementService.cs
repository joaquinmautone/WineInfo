using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.Entities;

namespace WineInfo.Services
{
    public interface IMesurementService
    {
        Task<IEnumerable<Mesurement>> GetMesurementsAsync();
        Mesurement AddMesurement(Mesurement mesurement);
        Mesurement GetMesurementById(int id);
        Mesurement UpdateMesurement(Mesurement mesurement);
        void DeleteMesurement(int id);
    }
}
