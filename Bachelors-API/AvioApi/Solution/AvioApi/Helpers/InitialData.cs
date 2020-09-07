using AvioApi.Data.Database;
using AvioApi.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvioApi.Data
{
    public class InitialData
    {   
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();
                context.Database.Migrate();

                if (context.AirCompanies.Any())
                    return;

                var companyRatings = GetCompanyRatings().ToArray();
                context.AvioCompanyRatings.AddRange(companyRatings);
                context.SaveChanges();

                var branches = GetBranches().ToArray();
                context.AirCompanyBranches.AddRange(branches);
                context.SaveChanges();

                var destinations = GetDestinations().ToArray();
                context.Destinations.AddRange(destinations);
                context.SaveChanges();

                var flights = GetFlights(context).ToArray();
                context.Flights.AddRange(flights);
                context.SaveChanges();

                var aircompanies = GetAirCompanies(context).ToArray();
                context.AirCompanies.AddRange(aircompanies);
                context.SaveChanges();

                LoadMapStrings(context);
            }
        }

        public static void LoadMapStrings(DataContext db)
        {
            var branches = db.AirCompanyBranches.ToList();

            foreach (var br in branches)
            {
                var address = br.Address.Replace(' ', '+');
                br.MapString = $"https://maps.google.com/maps?q={address}&output=embed";
            }
            db.SaveChanges();
        }

        public static List<Destination> GetDestinations()
        {
            List<Destination> destinations = new List<Destination>()
            { 
                new Destination { City = "Novi Sad", Country = "Serbia" },
                new Destination { City = "Belgrade", Country = "Serbia" },
                new Destination { City = "Bologna", Country = "Italy" },
                new Destination { City = "Vienna", Country = "Austria" },
                new Destination { City = "Malmo", Country = "Sweden" },
                new Destination { City = "Berlin", Country = "Germany" },
                new Destination { City = "Las Vegas", Country = "USA" },
                new Destination { City = "Frankfurt", Country = "Germany" },
                new Destination { City = "Paris", Country = "France" },
                new Destination { City = "Amsterdam", Country = "Netherlands" }
            };

            return destinations;
        }
        public static List<Branch> GetBranches()
        {
            List<Branch> branches = new List<Branch>()
            {
                new Branch { City = "Novi Sad", Country = "Serbia", Address ="Zmaj Jovina 6" },
                new Branch { City = "Belgrade", Country = "Serbia", Address  ="Marijane Gregoran 60" },
                new Branch { City = "Bologna", Country = "Italy", Address = "Via Vicenza 122"},
                new Branch { City = "Vienna", Country = "Austria", Address = "Nordbahnstraße 12" },
                new Branch { City = "Malmo", Country = "Sweden", Address = "Marieholmsvägen 10B" },
                new Branch { City = "Berlin", Country = "Germany", Address = "Wilmersdorfer Str 92" },
                new Branch { City = "Las Vegas", Country = "USA", Address = "Vegas Dr 3" },
                new Branch { City = "Frankfurt", Country = "Germany", Address = "Blanchardstraße 6" },
                new Branch { City = "Paris", Country = "France", Address = "Place de la Madeleine 48" },
                new Branch { City = "Amsterdam", Country = "Netherlands", Address = "Pieter Cornelisz Hooftstraat 62" }
            };

            return branches;
        }
        public static List<AirCompany> GetAirCompanies(DataContext db)
        {
            List<AirCompany> airCompanies = new List<AirCompany>()
            {
                new AirCompany {Name = "Qatar Airways", HeadOffice=db.AirCompanyBranches.Skip(7).First(), AverageGrade = 10,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/qatar_zaqh20.png", Incomes = new List<AvioIncomes>(), Flights = new List<Flight>(db.Flights.Take(100)),
                    PromoDescription = "We are in this together", Ratings = new List<CompanyRating>(db.AvioCompanyRatings.Skip(50).Take(10)) },
               
                new AirCompany {Name = "Singapore Airlines", HeadOffice=db.AirCompanyBranches.Skip(5).First(), AverageGrade = 9.2,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/singapore_axr4hw.png", Incomes = new List<AvioIncomes>(),Flights = new List<Flight>(db.Flights.Skip(100).Take(100)),
                    PromoDescription = "Enjoy world-class service", Ratings = new List<CompanyRating>(db.AvioCompanyRatings.Skip(60).Take(10))},
           
                new AirCompany {Name = "Emirates", HeadOffice=db.AirCompanyBranches.Skip(4).First(), AverageGrade = 8.9,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309404/emirates_trswql.png",Incomes = new List<AvioIncomes>(), Flights = new List<Flight>(db.Flights.Skip(200).Take(100)),
                    PromoDescription = "Choose Emirates airline to enjoy our world-class service on all flights",
                    Ratings = new List<CompanyRating>(db.AvioCompanyRatings.Skip(70).Take(10))},
             
                new AirCompany {Name = "Lufthansa", HeadOffice=db.AirCompanyBranches.Skip(2).First(), AverageGrade = 8.4,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/lufthansa_rvpbcd.png",Incomes = new List<AvioIncomes>(), Flights = new List<Flight>(db.Flights.Skip(300).Take(100)),
                    PromoDescription = "The Lufthansa Group is an aviation group with operations worldwide",
                    Ratings = new List<CompanyRating>(db.AvioCompanyRatings.Skip(80).Take(10))},
             
                new AirCompany {Name = "Air Serbia", HeadOffice=db.AirCompanyBranches.Skip(8).First(), AverageGrade = 7.6,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/serbia_j3ouuo.png",Incomes = new List<AvioIncomes>(), Flights = new List<Flight>(db.Flights.Skip(400).Take(100)),
                    PromoDescription = "Air Serbia has been a leader in air transport since the company was founded in 1927",
                    Ratings = new List<CompanyRating>(db.AvioCompanyRatings.Skip(90).Take(10))}
            };
                                                                                                                                                                
            return airCompanies;
        }
        public static List<Flight> GetFlights(DataContext db)
        {
            Random random = new Random();

            List<Flight> flights = new List<Flight>();

            for (int i = 0; i < 600; i++)
            {
                var departureDate = DateTime.Now.AddDays(random.Next(1, 15)).AddHours(random.Next(1, 14)).AddMinutes(random.Next(1, 59)); 
                var arrivalDate = departureDate.AddHours(random.Next(1, 3)).AddMinutes(random.Next(1, 59));
                var ticketPrice = random.Next(100, 550);
                var luggagePrice = random.Next(14, 70);
                var mileage = random.Next(100, 1500);
                var avgGrade = random.Next(6, 10);
                var travelTime = (arrivalDate - departureDate).TotalMinutes;
                var discountRandom = random.Next(1, 10);
                var discount = false;
                var ratings = GetFlightRatings();
                if (discountRandom == 1)
                    discount = true;

                Flight flight = new Flight {
                    DepartureDestination = db.Destinations.Skip(random.Next(1, 9)).First(), 
                    ArrivalDestination = db.Destinations.Skip(random.Next(1, 9)).First(), 
                    DepartureTime = departureDate,
                    ArrivalTime = arrivalDate,
                    TravelTime = travelTime, 
                    Ratings = ratings,
                    AverageGrade = avgGrade,
                    TicketPrice = ticketPrice,  
                    Mileage = mileage,
                    Luggage = luggagePrice,
                    Discount = discount
                };

                if(flight.DepartureDestination.City != flight.ArrivalDestination.City)
                {
                    flights.Add(flight);
                }
            }

            return flights;
        }
        public static List<FlightRating> GetFlightRatings()
        {
            Random r = new Random();

            List<FlightRating> ratings = new List<FlightRating>();

            for (int i = 0; i < 5; i++)
            {
                var value = r.Next(5, 11);

                FlightRating rating = new FlightRating() { Value = value };
                ratings.Add(rating);
            }

            return ratings;
        }

        public static List<CompanyRating> GetCompanyRatings()
        {
            Random r = new Random();

            List<CompanyRating> ratings = new List<CompanyRating>();

            for (int i = 0; i < 100; i++)
            {
                var value = r.Next(5, 11);

                CompanyRating rating = new CompanyRating() { Value = value };
                ratings.Add(rating);
            }

            return ratings;
        }
    }


}

