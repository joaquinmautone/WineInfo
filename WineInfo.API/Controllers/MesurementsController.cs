using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WineInfo.API.Models;
using WineInfo.Entities;
using WineInfo.Services;

namespace WineInfo.API.Controllers
{
    [Route("api/measurements")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMesurementService mesurementService;
        private readonly IMapper mapper;

        public MeasurementsController(IMesurementService mesurementService, IMapper mapper)
        {
            this.mesurementService = mesurementService ?? throw new ArgumentNullException(nameof(mesurementService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MesurementDto>>> GetMeasurementsAsync()
        {
            var mesurements = await mesurementService.GetMesurementsAsync();
            IEnumerable<MesurementDto> mesurementDto = mapper.Map<IEnumerable<MesurementDto>>(mesurements);

            return Ok(mesurementDto);
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetMeasurement(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
