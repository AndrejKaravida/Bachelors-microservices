using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using AvioApi.Helpers;
using AvioApi.Data.Repository;
using AvioApi.Domain.Entities;

namespace AvioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IFlightsRepository _repository;

        public ReservationsController(IFlightsRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("flightreservation")]
        public async Task<IActionResult> MakeFlightReservation([FromBody]JObject data)
        {
            int flightId = Int32.Parse(data["flightId"].ToString());
            Flight flight = _repository.GetFlight(flightId);

            var depDate = data["departureTime"].ToString();
            var arrDate = data["arrivalTime"].ToString();

            depDate = depDate.Replace('-', '/');
            arrDate = arrDate.Replace('-', '/');

            DateTime dep = Convert.ToDateTime(depDate);
            DateTime arr = Convert.ToDateTime(arrDate);

            var companyId = Int32.Parse(data["companyId"].ToString());
 
            FlightReservation reservation = new FlightReservation()
            {
                UserAuthId = data["authId"].ToString(),
                DepartureDestination = data["departureDestination"].ToString(),
                CompanyId = Int32.Parse(data["companyId"].ToString()),
                CompanyName = data["companyName"].ToString(),
                CompanyPhoto = data["companyPhoto"].ToString(),
                ArrivalDestination = data["arrivalDestination"].ToString(),
                DepartureDate = dep,
                Flight = flight,
                ArrivalDate = arr, 
                Price = Double.Parse(data["price"].ToString()),
                TravelLength = Double.Parse(data["travelLength"].ToString()), 
                Status = "Active"
            };

            var companyFromRepo =  _repository.GetCompany(companyId);
            AvioIncomes newIncome = new AvioIncomes() { Date = DateTime.Now, Value = reservation.Price };

            if (companyFromRepo.Incomes == null)
                companyFromRepo.Incomes = new List<AvioIncomes>();
            companyFromRepo.Incomes.Add(newIncome);

            _repository.Add(reservation);
        
            await _repository.SaveAll();

            return Ok();
        }

        [HttpGet("flightReservations/{authId}")]
        public IActionResult GetFlightReservationsForUser(string authId)
        {

            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != authId &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin1 &&
                User.FindFirst(ClaimTypes.NameIdentifier).Value != SystemAdminData.SysAdmin2)
                return Unauthorized();

            var reservations = _repository.GetFlightReservationsForUser(authId);

            return Ok(reservations);
        }
    }
}