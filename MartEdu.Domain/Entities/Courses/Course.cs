using MartEdu.Domain.Commons;
using MartEdu.Domain.Enums;
using MartEdu.Domain.Enums.Courses;
using MartEdu.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace MartEdu.Domain.Entities.Courses
{
    public class Course : IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Score { get; set; }
        public int CountOfVotes { get; set; }
        public Hashtag Teg { get; set; }
        public Level Level { get; set; }
        public Section Section { get; set; }
        public IEnumerable<User> Participants { get; set; }


        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public ItemState State { get; set; }


        public void Create()
        {
            this.CreatedAt = DateTime.Now;
            this.State = ItemState.Created;
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Updated;
        }

        public void Delete()
        {
            this.UpdatedAt = DateTime.Now;
            this.State = ItemState.Deleted;
        }
    }
}
