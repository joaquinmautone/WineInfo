using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineInfo.API.Models;
using WineInfo.Services;

namespace WineInfo.API.Controllers
{
    [Authorize]
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

        [HttpGet("{id}", Name = "GetMeasurement")]
        public async Task<ActionResult<MesurementDto>> GetMeasurement(int id)
        {
            var mesurements = await mesurementService.GetMesurementByIdAsync(id);
            MesurementDto mesurementDto = mapper.Map<MesurementDto>(mesurements);

            return Ok(mesurementDto);
        }

        [HttpPost]
        public async Task<ActionResult<MesurementDto>> Post([FromBody] MesurementDto mesurement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mesurementEntity = mapper.Map<Entities.Mesurement>(mesurement);
            var response = await mesurementService.AddMesurementAsync(mesurementEntity);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            mesurement = mapper.Map<MesurementDto>(response.Mesurement);

            return CreatedAtRoute("GetMeasurement", new { id = response.Mesurement.Id }, mesurement);
        }
    }
}
