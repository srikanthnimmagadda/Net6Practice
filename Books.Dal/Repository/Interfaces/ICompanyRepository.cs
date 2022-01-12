using Books.Models;

namespace Books.Dal.Repository.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void Update(Company obj);
    }
}
