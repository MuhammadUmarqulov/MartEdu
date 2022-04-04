using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Users;
using MartEdu.Domain.Enums;
using MartEdu.Domain.Enums.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MartEdu.Domain.Entities.Courses
{
    public class Course : IAuditable
    {
        public Course()
        {
            Participants = new List<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public long Score { get; set; }
        public int CountOfVotes { get; set; }
        public Hashtag Teg { get; set; }
        public Level Level { get; set; }
        public Section Section { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public string Image { get; set; }

        [Key]
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
