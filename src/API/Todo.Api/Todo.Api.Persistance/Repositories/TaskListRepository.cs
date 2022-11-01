using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Helpers;
using Todo.Api.Application.Models.UserNotification;
using Todo.Api.Domain.Entities;
using Todo.Api.Persistance.Models;
using Task = System.Threading.Tasks.Task;

namespace Todo.Api.Persistance.Repositories
{
    public class TaskListRepository : BaseRepository<TaskList>, ITaskListRepository
    {
        private readonly TodoApiDbContext _context;

        private readonly IMapper _mapper;

        public TaskListRepository(TodoApiDbContext context, IMapper mapper) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public Task<List<UserNotification>> GetDataForNotificationAsync()
        {
            var userNotifications = new List<UserNotification>();

            var now = DateTimeOffset.Now;

            var midnight = now.Date;

            var midnightAdd = now.AddDays(1).Date;

            var midnightRemove = now.AddDays(-1).Date;

            // take all timezones for which datetimeoffset is midnight
            var taskListIds = _context.TaskLists.ToList().
              Where(t =>
                (now.ConvertToTimezone(t.TimeZone) > midnightAdd &&
                DateTimeOffsetCompareWithoutSeconds(now.ConvertToTimezone(t.TimeZone), midnightAdd) &&
                t.DailyList.ConvertToTimezone(t.TimeZone) > midnight &&
                t.DailyList.ConvertToTimezone(t.TimeZone) < midnightAdd)
                ||
                (now.ConvertToTimezone(t.TimeZone) > midnightRemove &&
                DateTimeOffsetCompareWithoutSeconds(now.ConvertToTimezone(t.TimeZone), midnight) &&
                t.DailyList.ConvertToTimezone(t.TimeZone) > midnightRemove &&
                t.DailyList.ConvertToTimezone(t.TimeZone) < midnight)
                ||
                (now.ConvertToTimezone(t.TimeZone) == now &&
                DateTimeOffsetCompareWithoutSeconds(now.ConvertToTimezone(t.TimeZone), midnight) &&
                t.DailyList.ConvertToTimezone(t.TimeZone).Date == midnightAdd.Date))
             .Select(x => new
             {
                 x.Id,
                 x.CreatedByUserId
             })
             .AsEnumerable()
             .ToDictionary(
                kvp => kvp.Id,
                kvp => kvp.CreatedByUserId);


            taskListIds.ToList()
              .ForEach(x =>
                userNotifications.Add(
                    new UserNotification
                     (_context.Tasks
                        .Where(p =>
                            p.TaskListId == x.Key &&
                            p.IsDone == true)
                        .Count(),
                        x.Value)));

            return Task.FromResult(userNotifications);
        }

        public async Task<List<TaskList>> GetItemsAsync(Application.Models.Filters.ListFilterDto filterDto)
        {
            var filter = _mapper.Map<ListFilterDto>(filterDto);

            var query = _context.TaskLists
                .AsNoTracking()
                .AsQueryable(); ;

            query = ApplyFilter(query, filter);

            var page = filter.Page;
            var size = filter.PageSize;

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<string> GetTimeZoneAsync(int Id)
        {
            return await _context.TaskLists
                .Where(x => x.Id == Id)
                .Select(x => x.TimeZone)
                .FirstOrDefaultAsync() ?? throw new Exception(nameof(GetTimeZoneAsync));
        }

        private IQueryable<TaskList> ApplyFilter(IQueryable<TaskList> source, ListFilterDto filter)
        {
            source = source.Where(a => a.CreatedByUserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.Title))
            {
                source = source.Where(a => a.Title
                    .ToUpper()
                    .Contains(filter.Title.ToUpper()));
            }

            if (filter.Date.HasValue)
            {
                var date = filter.Date.Value;
                source = source.Where(a =>
                    a.DailyList.Date == date.Date);
            }

            return source;
        }

        private bool DateTimeOffsetCompareWithoutSeconds(DateTimeOffset dateTimeOffset1, DateTimeOffset dateTimeOffset2)
        {
            if (dateTimeOffset1.Year == dateTimeOffset2.Year &&
                dateTimeOffset1.Month == dateTimeOffset2.Month &&
                dateTimeOffset1.Day == dateTimeOffset2.Day &&
                dateTimeOffset1.Hour == dateTimeOffset2.Hour &&
                dateTimeOffset1.Minute == dateTimeOffset2.Minute)
            {
                return true;
            }

            return false;
        }
    }
}