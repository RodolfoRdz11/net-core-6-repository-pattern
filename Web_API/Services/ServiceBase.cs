using AutoMapper;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Services
{
    public interface IServiceBase<Repository, Entity, Request, Response>
    {
        Task<ResponseMetadata<Response>> GetAll(int? page, int? pageSize);
        Task<Response> GetOne(Guid id);
        Task<Response> Create(Request request, Guid createdById);
        Task Update(Request request, Guid updatedById);
        Task Delete(Guid id, Guid deletedById);
        Task Save();
    }

    public class ServiceBase<Repository, Entity, Request, Response> : IServiceBase<Repository, Entity, Request, Response> where Repository : IRepositoryBase<Entity> where Entity : class, new() where Request : class where Response : class
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repositoryWrapper;
        private Repository repository;
        private IUnitOfWork unitOfWork;
        protected Entity entity;

        public ServiceBase(IRepositoryWrapper repositoryWrapper, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repositoryWrapper = repositoryWrapper;
            this.unitOfWork = unitOfWork;
            var _repositoryWrapper = repositoryWrapper.GetType().GetProperties().Where(a => a.PropertyType == typeof(Repository)).FirstOrDefault();
            repository = (Repository)_repositoryWrapper?.GetValue(repositoryWrapper)!;
            entity = new Entity();
        }

        public async Task<ResponseMetadata<Response>> GetAll(int? page, int? pageSize)
        {
            List<Entity> entities;
            Metadata metadata = new Metadata();

            if (page != null && pageSize != null)
            {
                entities = await repository.FindAll()
                    //.Where(e => e.GetType().GetProperty("Deleted")?.GetValue(e) == false)
                    .Skip(((int)page - 1) * (int)pageSize)
                    .ToListAsync();

                metadata.TotalResults = repository.FindAll().Count();
                metadata.TotalPages = ((metadata.TotalResults + (int)pageSize - 1) / (int)pageSize);
            }
            else
            {
                entities = await repository.FindAll()
                    //.Where(c => c.Deleted == false)
                    .ToListAsync();
            }

            return new ResponseMetadata<Response>()
            {
                Data = mapper.Map<List<Entity>, List<Response>>(entities),
                Meta = metadata
            };
        }

        public async Task<Response> GetOne(Guid id)
        {
            var _entity = await repository.FindById(id);
            return mapper.Map<Response>(_entity);
        }

        public async Task<Response> Create(Request request, Guid createdById)
        {
            var _entity = mapper.Map<Entity>(request);
            _entity?.GetType()?.GetProperty("CreatedById")?.SetValue(_entity, createdById);

            await repositoryWrapper.Set<Entity>().Create(_entity!);
            await Save();
            return mapper.Map<Response>(_entity);
        }

        public async Task Update(Request request, Guid updatedById)
        {
            var _entity = mapper.Map<Entity>(request);
            _entity?.GetType()?.GetProperty("UpdatedById")?.SetValue(_entity, updatedById);
            _entity?.GetType()?.GetProperty("UpdatedAt")?.SetValue(_entity, Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd H:mm")));

            await repositoryWrapper.Set<Entity>().Create(_entity!);
            await Save();
        }

        public async Task Delete(Guid id, Guid deletedById)
        {
            var _entity = await repository.FindById(id);
            _entity?.GetType()?.GetProperty("Deleted")?.SetValue(_entity, true);
            _entity?.GetType()?.GetProperty("DeletedById")?.SetValue(_entity, deletedById);
            _entity?.GetType()?.GetProperty("DeletedAt")?.SetValue(_entity, Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd H:mm")));

            repositoryWrapper.Set<Entity>().Update(_entity!);
            await Save();
        }

        public async Task Save()
        {
            await unitOfWork.CompleteAsync();
        }
    }
}
