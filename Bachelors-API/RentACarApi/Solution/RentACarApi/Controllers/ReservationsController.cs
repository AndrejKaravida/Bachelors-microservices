using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Data;
using RentACarApi.Dtos;
using RentACarApi.Helpers;
using RentACarApi.Domain;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IRentACarRepository _repo;
        private readonly IMapper _mapper;

        public ReservationsController(IRentACarRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("carreservation")]
        public async Task<IActionResult> MakeCarReservation([FromBody]CarReservtion data)
        {
            Vehicle vehicle = _repo.GetVehicle(data.VehicleId);

            data.Startdate = data.Startdate.Replace('-', '/');
            data.Enddate = data.Enddate.Replace('-', '/');

            DateTime start = DateTime.ParseExact(data.Startdate, "d/M/yyyy", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(data.Enddate, "d/M/yyyy", CultureInfo.InvariantCulture);

            foreach(var r in vehicle.ReservedDates)
            {
                if(r.Date >= start.Date && r.Date <= end.Date)
                {
                    return BadRequest("Concurency error");
                }
            }

            Reservation reservation = new Reservation()
            {
                UserAuthId = data.AuthId,
                Vehicle = vehicle,
                StartDate = start,
                EndDate = end,
                CompanyName = data.Companyname,
                StartingLocation = data.StartingLocation,
                ReturningLocation = data.ReturningLocation,
                CompanyId = data.CompanyId,
                NumberOfDays = data.Totaldays,
                TotalPrice = data.Totalprice,
                Status = "Active"
            };

            _repo.Add(reservation);

            if(vehicle.ReservedDates == null)
            {
                vehicle.ReservedDates = new List<ReservedDate>();
            }

            for(var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                ReservedDate date = new ReservedDate { Date = dt };
                vehicle.ReservedDates.Add(date);
            }
            
            var companyFromRepo = await _repo.GetCompany(data.CompanyId);
            Income newIncome = new Income() { Date = DateTime.Now, Value = reservation.TotalPrice };

            if (companyFromRepo.Incomes == null)
                companyFromRepo.Incomes = new List<Income>();
            companyFromRepo.Incomes.Add(newIncome);

            if (await _repo.SaveAll())
                return NoContent();

           return BadRequest("Saving reservation failed on save");
        }

        [HttpGet("{authId}")]
        public async Task<IActionResult> GetCarReservationsForUser(string authId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != authId &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin1 &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin2)
                return Unauthorized();

            var reservations = await _repo.GetCarReservationsForUser(authId);

            foreach (var res in reservations)
            {
                if(res.StartDate.Date >= DateTime.Now.Date)
                {
                    res.DaysLeft = (res.EndDate.Date - res.StartDate.Date).TotalDays;
                }
                else
                {
                    res.DaysLeft = (res.EndDate.Date - DateTime.Now.Date).TotalDays;
                    if (res.DaysLeft < 0)
                        res.DaysLeft = 0;
                }
            }

            var reservationsToReturn = _mapper.Map<List<ReservationToReturn>>(reservations); 
            return Ok(reservationsToReturn);
        }
    }
}