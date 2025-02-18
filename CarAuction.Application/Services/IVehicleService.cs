﻿using CarAuction.Models.Vehicle;

namespace CarAuction.Application.Services
{
    public interface IVehicleService
    {
        Task<bool> CreateVehicle(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetVehiclesByType(string type);

        Task<IEnumerable<Vehicle>> GetVehiclesByManufacturer(string manufacturer);

        Task<IEnumerable<Vehicle>> GetVehiclesByModel(string manufacturer);

        Task<IEnumerable<Vehicle>> GetVehiclesByYear(int year);

        Task<Vehicle?> GetVehiclesById(Guid id);

        Task<bool> DeleteVehicleById(Guid id);
    }
}
