using RentACarApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RentACarApi.Data
{
    public interface IRentACarRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<RentACarCompany> GetCompany(int id);
        Task<RentACarCompany> GetCompanyWithVehicles(int id);
        Vehicle GetVehicle(int id);
        PagedList<Vehicle> GetVehiclesForCompany(int companyId, VehicleParams vehicleParams);
        List<RentACarCompany> GetAllCompanies();
        List<Vehicle> GetDiscountedVehicles(int companyId);
        Task<List<Reservation>> GetCarReservationsForUser(string authId);
        List<Income> GetCompanyIncomes(int companyId);
        List<Reservation> GetCompanyReservations(int companyId);
        Reservation GetReservation(int id);
        Task<List<RentACarCompany>> GetCompaniesWithCriteria(SearchParams searchParams);
        Task<List<Branch>> GetBranches();
        List<Vehicle> GetDiscountedVehiclesForUser(int companyId, DiscountedVehiclesParams vehiclesParams);
    }
}
