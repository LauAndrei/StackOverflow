using Core.Entities;

namespace Infrastructure.Repositories;

public class TagRepository : GenericRepository<Tag>
{
    private readonly DatabaseContext _databaseContext;
    
    public TagRepository(DatabaseContext context) : base(context)
    {
        _databaseContext = context;
    }
}