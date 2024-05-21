using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Models.ViewModels.Create
{
	public class UserListViewModel
	{
		public Guid courseId {  get; set; }
		public IQueryable<UserAndSub> userAndSubs{ get; set; }
	}

	public class UserAndSub
	{
		public Subscription subscription { get; set; }
		public User user { get; set; }
	}
}
