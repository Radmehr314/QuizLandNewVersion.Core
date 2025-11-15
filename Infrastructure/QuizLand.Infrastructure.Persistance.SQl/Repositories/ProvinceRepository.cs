using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Provinces;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class ProvinceRepository : IProvinceRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public ProvinceRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Province province) => await _dataBaseContext.AddAsync(province);

    public async Task<Province> GetById(long id) => await _dataBaseContext.Provinces.FindAsync(id);

    public async Task<List<Province>> GetAll() => await _dataBaseContext.Provinces.ToListAsync();
}