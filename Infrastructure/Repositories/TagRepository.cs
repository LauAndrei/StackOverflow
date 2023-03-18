using Core.Entities;

namespace Infrastructure.Repositories;

public class TagRepository : GenericRepository<Tag>
{
    public TagRepository(DatabaseContext context) : base(context)
    {
    }
}