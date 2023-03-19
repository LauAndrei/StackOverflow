using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(DatabaseContext context) : base(context)
    {
    }
}