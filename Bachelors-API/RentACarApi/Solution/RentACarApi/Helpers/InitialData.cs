using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using RentACarApi.Domain;
using Microsoft.AspNetCore.Builder;
using RentACarApi.Data;

namespace RentACarApi
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

                if (context.RentACarCompanies.Any())
                    return;

                var vehicleRatings = GetVehicleRatings().ToArray();
                context.VehicleRatings.AddRange(vehicleRatings);
                context.SaveChanges();

                var companyRatings = GetCompanyRatings().ToArray();
                context.CompanyRatings.AddRange(companyRatings);
                context.SaveChanges();

                var vehicles = GetVehicles(context).ToArray();
                context.Vehicles.AddRange(vehicles);
                context.SaveChanges();

                var branches = GetBranches().ToArray();
                context.Branches.AddRange(branches);
                context.SaveChanges();

                var rentacarcompanies = GetRentACarCompanies(context).ToArray();
                context.RentACarCompanies.AddRange(rentacarcompanies);
                context.SaveChanges();

                LoadFirstDestinations(context);
                LoadHeadOffice(context);
                LoadMapStrings(context);
            }
        }

        public static void LoadFirstDestinations(DataContext db)
        {
             Random r = new Random();

            var companies = db.RentACarCompanies
                .Include(b=> b.Branches)
                .Include(v => v.Vehicles)
                .ToList();

            foreach(var company in companies)
            {
                foreach(var v in company.Vehicles)
                {
                    
                    v.CurrentDestination = company.Branches.Skip(r.Next(0,2)).FirstOrDefault().City;
                }
            }

            db.SaveChanges();
        }

        public static void LoadHeadOffice(DataContext db)
        {
            var companies = db.RentACarCompanies
                .Include(h => h.HeadOffice)
                .Include(b => b.Branches)
                .ToList();

            foreach (var company in companies)
            {
                company.HeadOffice = company.Branches.FirstOrDefault();
            }
            db.SaveChanges();
        }

        public static void LoadMapStrings(DataContext db)
        {
            var branches = db.Branches.ToList();

            foreach (var br in branches)
            {
                var address = br.Address.Replace(' ', '+');
                br.MapString = $"https://maps.google.com/maps?q={address}&output=embed";
            }
            db.SaveChanges();
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

        public static List<RentACarCompany> GetRentACarCompanies(DataContext db)
        {
            List<RentACarCompany> rentACarCompanies = new List<RentACarCompany>()
            {
                new RentACarCompany {Name = "Alamo rentals", AverageGrade = 9.1, 
                    Photo="https://res.cloudinary.com/andrejkaravida/image/upload/v1596309404/alamocompany_ebvyqa.png",
                    WeekRentalDiscount = 10, MonthRentalDiscount = 19, 
                    Incomes = new List<Income>(), PromoDescription = "The best Rental in town!", 
                    Ratings = new List<CompanyRating>(db.CompanyRatings.Take(10)),
                    Branches = new List<Branch>(db.Branches.Take(2)),
                    Vehicles = new List<Vehicle>(db.Vehicles.Take(14))},
              
                new RentACarCompany {Name = "Hertz rentals", AverageGrade = 9.4, 
                    Photo="https://res.cloudinary.com/andrejkaravida/image/upload/v1596309404/hertzcompany_jynrd0.png",
                    WeekRentalDiscount = 15, MonthRentalDiscount = 26, 
                    Incomes = new List<Income>(), PromoDescription = "Drive with professionals",
                    Ratings = new List<CompanyRating>(db.CompanyRatings.Skip(10).Take(10)),
                    Branches = new List<Branch>(db.Branches.Skip(2).Take(2)), 
                    Vehicles = new List<Vehicle>(db.Vehicles.Skip(14).Take(14))},
               
                new RentACarCompany {Name = "Enterprise rentals", AverageGrade = 9.1,
                    WeekRentalDiscount = 12, MonthRentalDiscount = 24, 
                    Incomes = new List<Income>(), PromoDescription = "Experience is in our name",
                    Ratings = new List<CompanyRating>(db.CompanyRatings.Skip(20).Take(10)),
                    Photo="https://res.cloudinary.com/andrejkaravida/image/upload/v1596309404/enterprisecompany_p0kcjm.png",
                    Branches = new List<Branch>(db.Branches.Skip(4).Take(2)), 
                    Vehicles = new List<Vehicle>(db.Vehicles.Skip(28).Take(14))},
              
                new RentACarCompany {Name = "Turo rentals", AverageGrade = 8.1,
                    WeekRentalDiscount = 14, MonthRentalDiscount = 22, 
                    Incomes = new List<Income>(), PromoDescription = "Dedicated to car rentals",
                    Ratings = new List<CompanyRating>(db.CompanyRatings.Skip(30).Take(10)),
                    Photo="https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/turo_rurctz.png",
                    Branches = new List<Branch>(db.Branches.Skip(6).Take(2)),
                    Vehicles = new List<Vehicle>(db.Vehicles.Skip(42).Take(14)) },
             
                new RentACarCompany {Name = "Europcar rentals", AverageGrade = 9.4,
                    WeekRentalDiscount = 8, MonthRentalDiscount = 18, 
                    Incomes = new List<Income>(), PromoDescription = "Moving your way",
                    Ratings = new List<CompanyRating>(db.CompanyRatings.Skip(40).Take(10)),
                    Photo="https://res.cloudinary.com/andrejkaravida/image/upload/v1596309405/europcar_mwf6m0.png",
                    Branches = new List<Branch>(db.Branches.Skip(8).Take(2)), 
                    Vehicles = new List<Vehicle>(db.Vehicles.Skip(56).Take(14))}
            };

            return rentACarCompanies;
        }

        public static List<Vehicle> GetVehicles(DataContext db)
        {
            List<Vehicle> vehicles = new List<Vehicle>()
            {

                // ---- COMPANY 1 -----

                new Vehicle {Manufacturer = "Genesis", Model = "G70", AverageGrade = 7.9,    Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/7_bl8xxt.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Take(5))},

                new Vehicle {Manufacturer = "Alfa Romeo", Model = "Giulia", AverageGrade = 8.6,   Doors = 4, Seats = 5, Price = 369, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/1_tcu0pb.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(5).Take(5))},
              
                new Vehicle {Manufacturer = "Alfa Romeo", Model = "Quadrifoglio", AverageGrade = 8.8,  Doors = 4, Seats = 5, Price = 158, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/2_kljmod.jpg" , Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(10).Take(5))},
             
                new Vehicle {Manufacturer = "Audi", Model = "A5 Sportback", AverageGrade = 9.6,   Doors = 2, Seats = 2, Price = 347,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/3_p67msb.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(15).Take(5))},
               
                new Vehicle {Manufacturer = "BMW", Model = "M5", AverageGrade = 9.5,   Doors = 4, Seats = 4, Price = 369,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/28_nxdhjw.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(20).Take(5))},
                
                new Vehicle {Manufacturer = "BMW", Model = "M2 Competition", AverageGrade = 8.4,  Doors = 2, Seats = 5, Price = 318,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/29_phytsw.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(25).Take(5))},

                new Vehicle {Manufacturer = "Chevrolet", Model = "Corvette Z06", AverageGrade = 8.7,   Doors = 2, Seats = 2, Price = 313,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/30_yanhj2.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(30).Take(5))},
                  
                new Vehicle {Manufacturer = "Audi", Model = "A6", AverageGrade = 9.4,   Doors = 4, Seats = 5, Price = 395,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/4_msknxi.jpg", Type = "Large",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(35).Take(5))},

                new Vehicle {Manufacturer = "Audi", Model = "A7", AverageGrade = 8.6,    Doors = 4, Seats = 5, Price = 390,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/5_tdc77l.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(40).Take(5))},

                new Vehicle {Manufacturer = "Hyundai", Model = "Veloster N", AverageGrade = 8.6,   Doors = 4, Seats = 5, Price = 296,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/18_hf11ai.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(45).Take(5))},

                new Vehicle {Manufacturer = "Mazda", Model = "3", AverageGrade = 8.8, Doors = 4, Seats = 5, Price = 289,
                    IsDeleted = false, OldPrice = 348, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/19_rxr4bg.jpg" , Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(50).Take(5))},
               
                new Vehicle {Manufacturer = "Mercedes", Model = "AMG E53 coupe", AverageGrade = 9.4,   Doors = 4, Seats = 5, Price = 268,
                    IsDeleted = false, OldPrice = 395, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/21_f0qkep.jpg", Type = "Large",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(55).Take(5))},
                
                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf GTI", AverageGrade = 9.9,   Doors = 4, Seats = 5, Price = 148,
                    IsDeleted = false, OldPrice = 210, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/23_khkbkc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(60).Take(5))},
                
                new Vehicle {Manufacturer = "Volvo", Model = "V90", AverageGrade = 7.6, Doors = 4, Seats = 5, Price = 270,
                    IsDeleted = false, OldPrice = 320, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/25_m0tk2u.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(65).Take(5))},



                // ---- COMPANY 2 -----

                new Vehicle {Manufacturer = "Audi", Model = "A6", AverageGrade = 9.4,   Doors = 4, Seats = 5, Price = 395, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/4_msknxi.jpg", Type = "Large",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(70).Take(5))},
              
                new Vehicle {Manufacturer = "Audi", Model = "A7", AverageGrade = 8.6,    Doors = 4, Seats = 5, Price = 390, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/5_tdc77l.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(75).Take(5))},
              
                new Vehicle {Manufacturer = "Audi", Model = "A8", AverageGrade = 9.9,   Doors = 4, Seats = 5, Price = 399, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/6_yhdntn.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(80).Take(5))},
               
                new Vehicle {Manufacturer = "Chevrolet", Model = "Corvette ZR1", AverageGrade = 9.6,   Doors = 2, Seats = 4, Price = 380,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/31_ytbmei.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(85).Take(5))},
               
                new Vehicle {Manufacturer = "Chevrolet", Model = "Spark", AverageGrade = 7.6,  Doors = 2, Seats = 5, Price = 230,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/32_wcuhge.jpg", Type = "Small", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(90).Take(5))},
                 
                new Vehicle {Manufacturer = "Chevrolet", Model = "Volt", AverageGrade = 8.9,   Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/33_zmdgwo.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(95).Take(5))},
                
                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf GTI", AverageGrade = 9.9,   Doors = 4, Seats = 5, Price = 148,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/23_khkbkc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(100).Take(5))},

                new Vehicle {Manufacturer = "Volvo", Model = "V90", AverageGrade = 7.6,  Doors = 4, Seats = 5, Price = 270,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/25_m0tk2u.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(105).Take(5))},
                
                new Vehicle {Manufacturer = "Ford", Model = "Mustang Shelby GT350", AverageGrade = 8.6,   Doors = 2, Seats = 2, Price = 285,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/34_pky561.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(110).Take(5))},
                
                new Vehicle {Manufacturer = "Mitsubishi", Model = "Mirage", AverageGrade = 8.2,  Doors = 4, Seats = 4, Price = 365,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/17_oyw7vp.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(115).Take(5))},



                new Vehicle {Manufacturer = "Mercedes", Model = "AMG C43 Sedan", AverageGrade = 9.6,   Doors = 4, Seats = 5, Price = 296,
                    IsDeleted = false, OldPrice = 347, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/20_qqmhpu.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(120).Take(5))},

                new Vehicle {Manufacturer = "Mercedes", Model = "AMG E53 coupe", AverageGrade = 9.4,   Doors = 4, Seats = 5, Price = 268,
                    IsDeleted = false, OldPrice = 395, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/21_f0qkep.jpg", Type = "Large",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(125).Take(5))},

                new Vehicle {Manufacturer = "Honda", Model = "Accord", AverageGrade = 7.6,   Doors = 4, Seats = 5, Price = 130,
                    IsDeleted = false, OldPrice = 185, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/12_th3bzc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(130).Take(5))},

                new Vehicle {Manufacturer = "Toyota", Model = "Yaris", AverageGrade = 8.6,   Doors = 4, Seats = 4, Price = 120,
                    IsDeleted = false, OldPrice = 179,  IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/13_vndqzb.jpg", Type = "Small",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(135).Take(5))},





                // ---- COMPANY 3 -----


                new Vehicle {Manufacturer = "Genesis", Model = "G70", AverageGrade = 7.9,    Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/7_bl8xxt.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(140).Take(5))},
               
                new Vehicle {Manufacturer = "BMW ", Model = "2-series", AverageGrade = 7.6,  Doors = 2, Seats = 5, Price = 290, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/8_cncjuw.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(145).Take(5))},
              
                new Vehicle {Manufacturer = "Chevrolet", Model = "Corvette", AverageGrade = 7.9,    Doors = 2, Seats = 5, Price = 390, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/10_rcxihl.jpg", Type = "Small", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(150).Take(5))},
              
                new Vehicle {Manufacturer = "Ford", Model = "Mustang", AverageGrade = 8.6,  Doors = 4, Seats = 5, Price = 380, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/11_eugdzb.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(155).Take(5))},
               
                new Vehicle {Manufacturer = "Honda", Model = "Accord Hybrid", AverageGrade = 8.2,   Doors = 4, Seats = 5, Price = 290,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/35_sbcatw.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(160).Take(5))},
               
                new Vehicle {Manufacturer = "Honda", Model = "Civic Sedan", AverageGrade = 8.4,  Doors = 4, Seats = 5, Price = 270,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/17_oyw7vp.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(165).Take(5))},

                new Vehicle {Manufacturer = "Honda", Model = "Civic Hatchback", AverageGrade = 8.5, Doors = 4, Seats = 4, Price = 220,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/37_xxj67t.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(170).Take(5))},
               
                new Vehicle {Manufacturer = "Audi", Model = "A7", AverageGrade = 8.6, Doors = 4, Seats = 5, Price = 390,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/5_tdc77l.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(175).Take(5))},

                new Vehicle {Manufacturer = "Hyundai", Model = "Veloster N", AverageGrade = 8.6,   Doors = 4, Seats = 5, Price = 296,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/18_hf11ai.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(180).Take(5))},

                new Vehicle {Manufacturer = "Honda", Model = "Insight", AverageGrade = 7.9, Doors = 4, Seats = 5, Price = 231,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/38_ygrxwv.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(185).Take(5))},

                new Vehicle {Manufacturer = "Porsche", Model = "911 GT3 RS", AverageGrade = 8.6,    Doors = 2, Seats = 3, Price = 310,
                    IsDeleted = false, OldPrice = 400, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/22_vm61ph.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(190).Take(5))},

                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf GTI", AverageGrade = 9.9,   Doors = 4, Seats = 5, Price = 148,
                    IsDeleted = false, OldPrice = 210, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/23_khkbkc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(195).Take(5))},

                new Vehicle {Manufacturer = "Hyundai", Model = "Ioniq", AverageGrade = 7.6,  Doors = 4, Seats = 5, Price = 138,
                    IsDeleted = false, IsOnDiscount = true, OldPrice = 190,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/18_hf11ai.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(200).Take(5))},
               
                new Vehicle {Manufacturer = "Kia", Model = "Cadenza", AverageGrade = 9.6,  Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, IsOnDiscount = true, OldPrice = 360,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/39_nrol8x.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(205).Take(5))},



                // ---- COMPANY 4 -----


                new Vehicle {Manufacturer = "Honda", Model = "Accord", AverageGrade = 7.6,   Doors = 4, Seats = 5, Price = 130, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/12_th3bzc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(210).Take(5))},
              
                new Vehicle {Manufacturer = "Toyota", Model = "Yaris", AverageGrade = 8.6,   Doors = 4, Seats = 4, Price = 120, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/13_vndqzb.jpg", Type = "Small",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(215).Take(5))},
               
                new Vehicle {Manufacturer = "Chevrolet", Model = "Camaro", AverageGrade = 9.6, Doors = 2, Seats = 5, Price = 145, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/9_klyyrp.jpg", Type = "Small", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(220).Take(5))},
               
                new Vehicle {Manufacturer = "Ford", Model = "Fiesta", AverageGrade = 9.4, Doors = 4, Seats = 5, Price = 214, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309577/14_bn2ifs.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(225).Take(5))},
               
                new Vehicle {Manufacturer = "Kia", Model = "Rio Hatchback", AverageGrade = 8.4,  Doors = 4, Seats = 5, Price = 225,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/40_cdnitq.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(230).Take(5))},
                
                new Vehicle {Manufacturer = "Kia", Model = "Stinger", AverageGrade = 8.9,    Doors = 4, Seats = 5, Price = 245,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/41_c8wler.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(235).Take(5))},
                
                new Vehicle {Manufacturer = "Mazda", Model = "MX-5 Miata", AverageGrade = 8.6,    Doors = 2, Seats = 2, Price = 289,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/42_iu2wlh.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(240).Take(5))},
               
                new Vehicle {Manufacturer = "McLaren", Model = "570S GT", AverageGrade = 9.9,    Doors = 2, Seats = 2, Price = 400,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/43_zyxlg1.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(245).Take(5))},
               
                new Vehicle {Manufacturer = "Mercedes", Model = "AMG C63 cabriolet", AverageGrade = 8.9,    Doors = 4, Seats = 5, Price = 296,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/44_h5u4hj.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(250).Take(5))},
                
                new Vehicle {Manufacturer = "Mercedes", Model = "AMG E63", AverageGrade = 8.6,    Doors = 4, Seats = 5, Price = 330,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/45_xbb9mw.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(255).Take(5))},

                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf SportWagen", AverageGrade = 7.9,    Doors = 4, Seats = 5, Price = 180,
                    IsDeleted = false, OldPrice = 260, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/24_hsacxq.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(260).Take(5))},

                new Vehicle {Manufacturer = "Volvo", Model = "V90", AverageGrade = 7.6, Doors = 4, Seats = 5, Price = 270,
                    IsDeleted = false, OldPrice = 320, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/25_m0tk2u.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(265).Take(5))},
                
                new Vehicle {Manufacturer = "Mercedes", Model = "AMG E63 S Wagon", AverageGrade = 7.6,   Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, OldPrice = 330, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/46_uk0lna.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(270).Take(5))},
               
                new Vehicle {Manufacturer = "Mercedes", Model = "AMG S63/S65", AverageGrade = 9.3,   Doors = 4, Seats = 5, Price = 290,
                    IsDeleted = false, OldPrice = 350, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/27_v8lc5s.jpg", Type = "Luxury",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(275).Take(5))},


                // ---- COMPANY 5 -----

                new Vehicle {Manufacturer = "Nissan", Model = "Versa", AverageGrade = 8.6,    Doors = 4, Seats = 5, Price = 146, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/15_ft90kl.png", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(280).Take(5))},

                new Vehicle {Manufacturer = "Kia", Model = "Rio", AverageGrade = 8.3,    Doors = 4, Seats = 5, Price = 210, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/16_vpyskw.jpg", Type = "Small", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(285).Take(5))},
               
                new Vehicle {Manufacturer = "Mitsubishi", Model = "Mirage", AverageGrade = 8.2, Doors = 4, Seats = 4, Price = 365, 
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/17_oyw7vp.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(290).Take(5))},
               
                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf SportWagen", AverageGrade = 7.9,    Doors = 4, Seats = 5, Price = 180,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/24_hsacxq.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(295).Take(5))},

                new Vehicle {Manufacturer = "Volvo", Model = "V90", AverageGrade = 7.6, Doors = 4, Seats = 5, Price = 270,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/25_m0tk2u.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(300).Take(5))},

                new Vehicle {Manufacturer = "Mercedes", Model = "AMG E63 S Wagon", AverageGrade = 7.6,   Doors = 4, Seats = 5, Price = 260,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/46_uk0lna.jpg", Type = "Medium",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(305).Take(5))},

                new Vehicle {Manufacturer = "Mercedes", Model = "AMG S63/S65", AverageGrade = 9.3,   Doors = 4, Seats = 5, Price = 340,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/27_v8lc5s.jpg", Type = "Luxury",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(310).Take(5))},
               
                new Vehicle {Manufacturer = "Volkswagen", Model = "Golf GTI", AverageGrade = 9.9,   Doors = 4, Seats = 5, Price = 198,
                    IsDeleted = false, IsOnDiscount = false,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309578/23_khkbkc.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(315).Take(5))},

                new Vehicle {Manufacturer = "Hyundai", Model = "Ioniq", AverageGrade = 7.6, Doors = 4, Seats = 5, Price = 175,
                    IsDeleted = false, IsOnDiscount = false, 
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/18_hf11ai.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(320).Take(5))},

                new Vehicle {Manufacturer = "Kia", Model = "Cadenza", AverageGrade = 9.6,   Doors = 4, Seats = 5, Price = 320,
                    IsDeleted = false, IsOnDiscount = false, 
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309580/39_nrol8x.jpg", Type = "Medium",  Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(325).Take(5))},

                new Vehicle {Manufacturer = "Volvo", Model = "V90 Cross Country", AverageGrade = 7.9,    Doors = 4, Seats = 5, Price = 275,
                    IsDeleted = false, OldPrice = 260, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/26_uff7zr.jpg", Type = "Large", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(330).Take(5))},

                new Vehicle {Manufacturer = "Porsche", Model = "911 Turbo/Turbo S", AverageGrade = 8.6, Doors = 2, Seats = 3, Price = 310,
                    IsDeleted = false, OldPrice = 380, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/27_v8lc5s.jpg", Type = "Luxury", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(335).Take(5))},
                
                new Vehicle {Manufacturer = "Chevrolet", Model = "Corvette Z06", AverageGrade = 8.7,   Doors = 2, Seats = 2, Price = 313,
                    IsDeleted = false, OldPrice = 370, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309579/30_yanhj2.jpg", Type = "Medium", Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(340).Take(5))},

                new Vehicle {Manufacturer = "Audi", Model = "A6", AverageGrade = 9.4,  Doors = 4, Seats = 5, Price = 290,
                    IsDeleted = false, OldPrice = 360, IsOnDiscount = true,
                    Photo = "https://res.cloudinary.com/andrejkaravida/image/upload/v1596309576/4_msknxi.jpg", Type = "Large",Ratings = new List<VehicleRating>(db.VehicleRatings.Skip(345).Take(5))},
            };

            return vehicles;
        }

        public static List<VehicleRating> GetVehicleRatings()
        {
            Random r = new Random();

            List<VehicleRating> ratings = new List<VehicleRating>();

            for(int i = 0; i < 350; i++)
            {
                var value = r.Next(5, 11);

                VehicleRating rating = new VehicleRating() { Value = value };
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

