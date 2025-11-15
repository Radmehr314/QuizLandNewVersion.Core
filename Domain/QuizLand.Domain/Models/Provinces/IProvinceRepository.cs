namespace QuizLand.Domain.Models.Provinces;

public interface IProvinceRepository
{
 
    Task Add(Province province);
    Task<Province> GetById(long id);
    Task<List<Province>> GetAll();
    
}