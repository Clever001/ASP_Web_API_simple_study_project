using api.Models;

namespace WebApplication3.Interfaces;

public interface IStockRepository {
    Task<List<Stock>> GetAllAsync();
}