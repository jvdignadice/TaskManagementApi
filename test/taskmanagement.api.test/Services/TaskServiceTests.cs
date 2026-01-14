using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using taskmanagement.api.ms.domain.Interface;
using taskmanagement.api.ms.domain.Service;
using taskmanagement.api.ms.DTOs;
using taskmanagement.api.ms.Models;
using taskmanagement.api.ms.Models.Enums;

namespace TaskManagement.Tests
{
    public class TaskServiceTests
    {
        private readonly ITaskDbContext _dbContextFake;
        private readonly TaskService _taskService;
        private readonly DbSet<TaskItem> _tasksFake;

        public TaskServiceTests()
        {
            _dbContextFake = A.Fake<ITaskDbContext>();
            _tasksFake = A.Fake<DbSet<TaskItem>>(o => o.Implements(typeof(IQueryable<TaskItem>)));
            A.CallTo(() => _dbContextFake.Tasks).Returns(_tasksFake);

            _taskService = new TaskService(_dbContextFake);
        }

        [Fact]
        public async Task AddTask_ShouldAddTask_WhenDtoIsValid()
        {
            // Arrange
            var dto = new CreateTaskDto
            {
                Title = "New Task",
                Description = "Test Description",
                Priority = TaskPriority.Medium,
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            A.CallTo(() => _dbContextFake.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(1));

            // Act
            var result = await _taskService.AddTask(dto);

            // Assert
            result.Should().BeEquivalentTo(dto);
            A.CallTo(() => _tasksFake.Add(A<TaskItem>.Ignored)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task AddTask_ShouldThrowException_WhenSaveFails()
        {
            // Arrange
            var dto = new CreateTaskDto { Title = "Task Fail" };
            A.CallTo(() => _dbContextFake.SaveChangesAsync(CancellationToken.None))
                .ThrowsAsync(new Exception("DB Error"));

            // Act
            Func<Task> act = async () => await _taskService.AddTask(dto);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("DB Error");
        }
        [Fact]
        public async Task UpdateTask_ShouldUpdateTask_WhenTaskExists()
        {
            var existingTask = new TaskItem { Id = 1, Title = "Old", CreatedAt = DateTime.UtcNow };
            A.CallTo(() => _tasksFake.FindAsync(1)).Returns(new ValueTask<TaskItem>(existingTask));
            A.CallTo(() => _dbContextFake.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));

            var dto = new UpdateTaskDto { Title = "Updated", Description = "Updated Desc", Priority = TaskPriority.High, Status = TasksStatus.Pending, DueDate = DateTime.UtcNow };

            var result = await _taskService.UpdateTask(1, dto);

            result.Title.Should().Be(dto.Title);
            A.CallTo(() => _dbContextFake.SaveChangesAsync(CancellationToken.None)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task UpdateTask_ShouldThrow_WhenTaskNotFound()
        {
            A.CallTo(() => _tasksFake.FindAsync(999)).Returns(new ValueTask<TaskItem>((TaskItem)null));

            var dto = new UpdateTaskDto { Title = "Updated" };

            Func<Task> act = async () => await _taskService.UpdateTask(999, dto);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }
        [Fact]
        public async Task RemoveTask_ShouldRemoveTask_WhenTaskExists()
        {
            var task = new TaskItem { Id = 1 };
            A.CallTo(() => _tasksFake.FindAsync(1)).Returns(new ValueTask<TaskItem>(task));
            A.CallTo(() => _dbContextFake.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(1));

            var result = await _taskService.RemoveTask(1);

            result.Should().Be(task);
            A.CallTo(() => _tasksFake.Remove(task)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task RemoveTask_ShouldThrow_WhenTaskNotFound()
        {
            A.CallTo(() => _tasksFake.FindAsync(999)).Returns(new ValueTask<TaskItem>((TaskItem)null));

            Func<Task> act = async () => await _taskService.RemoveTask(999);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }
        [Fact]
        public async Task GetTaskById_ShouldReturnTask_WhenExists()
        {
            var task = new TaskItem { Id = 1 };
            A.CallTo(() => _tasksFake.FindAsync(1)).Returns(new ValueTask<TaskItem>(task));

            var result = await _taskService.GetTaskById(1);

            result.Should().Be(task);
        }
        [Fact]
        public async Task GetTaskById_ShouldReturnNewTask_WhenNotExists()
        {
            A.CallTo(() => _tasksFake.FindAsync(999)).Returns(new ValueTask<TaskItem>((TaskItem)null));

            var result = await _taskService.GetTaskById(999);

            result.Should().NotBeNull();
            result.Id.Should().Be(0);
        }
    }
}
